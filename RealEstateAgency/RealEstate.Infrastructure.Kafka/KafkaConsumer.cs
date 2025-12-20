using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.RealEstateRequests;
using System.Text.Json;

namespace RealEstate.Infrastructure.Kafka;

/// <summary>
/// Background Kafka consumer that reads JSON messages from a configured topic,
/// deserializes them into <see cref="RealEstateRequestCreateUpdateDto"/>,
/// and persists them using an application service.
/// </summary>
public class KafkaConsumer(
    ILogger<KafkaConsumer> logger,
    IConsumer<Ignore, string> consumer,
    IServiceScopeFactory scopeFactory,
    IOptions<KafkaConsumerSettings> options) : BackgroundService
{
    /// <summary>
    /// Kafka consumer settings from IOptions.
    /// </summary>
    private readonly KafkaConsumerSettings _settings = options.Value;

    /// <summary>
    /// Main execution loop. Subscribes to the topic, consumes messages until cancellation,
    /// and persists successfully processed DTOs.
    /// </summary>
    /// <param name="stoppingToken">Cancellation token.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            consumer.Subscribe(_settings.TopicName);
            logger.LogInformation("KafkaConsumer started on topic {TopicName}", _settings.TopicName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "KafkaConsumer failed to subscribe. TopicName={TopicName}", _settings.TopicName);
            return;
        }

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<Ignore, string>? message = null;

                try
                {
                    message = consumer.Consume(TimeSpan.FromMilliseconds(_settings.ConsumeTimeoutMs));

                    if (message is null)
                        continue;

                    var payload = message.Message?.Value;

                    if (string.IsNullOrWhiteSpace(payload))
                    {
                        logger.LogWarning("Kafka message has empty payload. Topic={Topic}, Partition={Partition}, Offset={Offset}",
                            message.Topic, message.Partition.Value, message.Offset.Value);

                        CommitIfNeeded(message);
                        continue;
                    }

                    RealEstateRequestCreateUpdateDto? dto = null;

                    for (var attempt = 1; attempt <= _settings.MaxDeserializeAttempts; attempt++)
                    {
                        try
                        {
                            dto = JsonSerializer.Deserialize<RealEstateRequestCreateUpdateDto>(payload);
                            break;
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex, "Deserialization attempt {Attempt}/{MaxAttempts} failed. Topic={Topic}, Partition={Partition}, Offset={Offset}",
                                attempt, _settings.MaxDeserializeAttempts, message.Topic, message.Partition.Value, message.Offset.Value);
                        }
                    }

                    if (dto == null)
                    {
                        logger.LogError("Message could not be deserialized after {MaxAttempts} attempts, skipping... Payload={Payload}",
                            _settings.MaxDeserializeAttempts, payload);

                        CommitIfNeeded(message);
                        continue;
                    }

                    using var scope = scopeFactory.CreateScope();
                    var service = scope.ServiceProvider.GetRequiredService<IApplicationService<RealEstateRequestDto, RealEstateRequestCreateUpdateDto, int>>();

                    try
                    {
                        var savedEntity = await service.Create(dto);

                        CommitIfNeeded(message);

                        logger.LogInformation("Kafka message processed and saved successfully. RequestId={Id}, ClientId={ClientId}, PropertyId={PropertyId}",
                            savedEntity.Id, dto.ClientId, dto.PropertyId);
                    }
                    catch (KeyNotFoundException ex)
                    {
                        logger.LogWarning(ex, "Skipping message because related entity was not found. ClientId={ClientId}, PropertyId={PropertyId}", dto.ClientId, dto.PropertyId);

                        CommitIfNeeded(message);
                    }
                }
                catch (ConsumeException ex) when (ex.Error.Code == ErrorCode.UnknownTopicOrPart)
                {
                    logger.LogWarning("Kafka topic is not available yet. TopicName={TopicName}. Retrying...", _settings.TopicName);
                    await Task.Delay(1000, stoppingToken);
                }
                catch (ConsumeException ex)
                {
                    logger.LogError(ex, "Kafka consume error. Reason={Reason}", ex.Error.Reason);
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("KafkaConsumer is stopping due to cancellation. TopicName={TopicName}", _settings.TopicName);
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unexpected error in KafkaConsumer while processing message.");
                }
            }
        }
        finally
        {
            try
            {
                consumer.Close();
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error while closing Kafka consumer.");
            }

            logger.LogInformation("KafkaConsumer stopped. TopicName={TopicName}", _settings.TopicName);
        }
    }

    /// <summary>
    /// Commits the consumed message offset if auto-commit is disabled.
    /// </summary>
    /// <param name="message">Consumed message to commit.</param>
    private void CommitIfNeeded(ConsumeResult<Ignore, string> message)
    {
        if (_settings.AutoCommitEnabled)
            return;

        try
        {
            consumer.Commit(message);
        }
        catch (KafkaException ex)
        {
            logger.LogWarning(ex, "Commit failed. Topic={Topic}, Partition={Partition}, Offset={Offset}", message.Topic, message.Partition.Value, message.Offset.Value);
        }
    }
}