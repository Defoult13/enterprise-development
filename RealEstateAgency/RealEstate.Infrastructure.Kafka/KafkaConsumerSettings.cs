namespace RealEstate.Infrastructure.Kafka;

/// <summary>
/// Kafka settings used by RealEstate Kafka consumer host.
/// </summary>
public class KafkaConsumerSettings
{
    /// <summary>
    /// Kafka topic name used for producing and consuming messages.
    /// </summary>
    public string TopicName { get; init; } = "real-estate";

    /// <summary>
    /// Enables Kafka auto-commit for the consumer.
    /// If false, the consumer commits offsets manually after successful processing.
    /// </summary>
    public bool AutoCommitEnabled { get; init; } = false;

    /// <summary>
    /// Poll timeout for consuming messages in milliseconds.
    /// </summary>
    public int ConsumeTimeoutMs { get; init; } = 250;

    /// <summary>
    /// Maximum number of attempts to deserialize a message payload.
    /// </summary>
    public int MaxDeserializeAttempts { get; init; } = 3;
}