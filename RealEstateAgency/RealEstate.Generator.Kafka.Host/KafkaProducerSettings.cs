namespace RealEstate.Generator.Kafka.Host;

/// <summary>
/// Kafka settings used by the RealEstate Kafka producer host.
/// </summary>
public class KafkaProducerSettings
{
    /// <summary>
    /// Kafka topic name used for producing messages.
    /// </summary>
    public string TopicName { get; init; } = string.Empty;

    /// <summary>
    /// Maximum number of attempts to send a message.
    /// </summary>
    public int MaxProduceAttempts { get; init; } = 3;

    /// <summary>
    /// Delay between produce retries in milliseconds.
    /// </summary>
    public int RetryDelayMs { get; init; } = 250;
}