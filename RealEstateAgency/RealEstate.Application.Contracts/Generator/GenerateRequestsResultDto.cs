namespace RealEstate.Application.Contracts.Generator;

/// <summary>
/// Result of generating and publishing real-estate request messages to Kafka.
/// </summary>
/// <param name="TotalRequested">Total number of items requested for generation.</param>
/// <param name="TotalSent">Total number of items successfully sent.</param>
/// <param name="BatchSize">Configured number of items per batch.</param>
/// <param name="DelayMs">Configured delay between batches in milliseconds.</param>
/// <param name="Batches">Number of batches processed.</param>
/// <param name="Canceled">Indicates whether the operation was canceled before sending all items.</param>
public sealed record GenerateRequestsResultDto(
    int TotalRequested,
    int TotalSent,
    int BatchSize,
    int DelayMs,
    int Batches,
    bool Canceled
);