using Confluent.Kafka;
using Microsoft.Extensions.Options;
using RealEstate.Application.Contracts.RealEstateRequests;
using System.Text.Json;

namespace RealEstate.Generator.Kafka.Host;

/// <summary>
/// Kafka producer that serializes <see cref="RealEstateRequestCreateUpdateDto"/> into JSON
/// and publishes messages to a configured topic.
/// </summary>
/// <param name="logger">Logger instance.</param>
/// <param name="producer">Kafka producer.</param>
/// <param name="options">Producer settings.</param>
public class KafkaProducer(
    ILogger<KafkaProducer> logger,
    IProducer<Null, string> producer,
    IOptions<KafkaProducerSettings> options)
{
    private readonly KafkaProducerSettings _settings = options.Value;

    /// <summary>
    /// Sends a request DTO as a JSON message to Kafka.
    /// </summary>
    /// <param name="dto">Request DTO to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task Produce(RealEstateRequestCreateUpdateDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_settings.TopicName))
            throw new InvalidOperationException("KafkaProducerSettings.TopicName must be configured.");

        var payload = JsonSerializer.Serialize(dto);

        for (var attempt = 1; attempt <= _settings.MaxProduceAttempts; attempt++)
        {
            try
            {
                var result = await producer.ProduceAsync(_settings.TopicName, new Message<Null, string> { Value = payload }, cancellationToken);

                logger.LogInformation("Kafka message produced successfully. Topic={Topic}, Partition={Partition}, Offset={Offset}, ClientId={ClientId}, PropertyId={PropertyId}",
                    result.Topic, result.Partition.Value, result.Offset.Value, dto.ClientId, dto.PropertyId);

                return;
            }
            catch (ProduceException<Null, string> ex) when (attempt < _settings.MaxProduceAttempts)
            {
                logger.LogWarning(ex,
                    "Kafka produce attempt {Attempt}/{MaxAttempts} failed. Reason={Reason}. Retrying in {Delay}ms...",
                    attempt, _settings.MaxProduceAttempts, ex.Error.Reason, _settings.RetryDelayMs);

                await Task.Delay(_settings.RetryDelayMs, cancellationToken);
            }
            catch (ProduceException<Null, string> ex)
            {
                logger.LogError(ex,
                    "Kafka produce failed after {MaxAttempts} attempts. Reason={Reason}. ClientId={ClientId}, PropertyId={PropertyId}",
                    _settings.MaxProduceAttempts, ex.Error.Reason, dto.ClientId, dto.PropertyId);

                throw;
            }
        }
    }

    /// <summary>
    /// Sends a batch of request DTOs as JSON messages to Kafka in parallel.
    /// </summary>
    /// <param name="dtos">Request DTOs to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public async Task ProduceMany(IList<RealEstateRequestCreateUpdateDto> dtos, CancellationToken cancellationToken = default)
    {
        var tasks = dtos.Select(dto => Produce(dto, cancellationToken));
        await Task.WhenAll(tasks);
    }
}