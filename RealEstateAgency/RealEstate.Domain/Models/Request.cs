﻿namespace RealEstateAgency.Domain.Models;

/// <summary>
/// A client request to buy or sell a specific real-estate object.
/// Pure domain model for in-memory usage (no ORM concerns).
/// </summary>
public sealed class Request
{
    /// <summary>
    /// Integer identifier assigned explicitly in seed/data layer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Client who placed the request.
    /// </summary>
    public required Counterparty Client { get; init; }

    /// <summary>
    /// Real-estate object the request refers to.
    /// </summary>
    public required RealEstateObject Property { get; init; }

    /// <summary>
    /// Request type: buy or sell.
    /// </summary>
    public required RequestType Type { get; init; }

    /// <summary>
    /// Monetary amount stated in the request.
    /// </summary>
    public required decimal Amount { get; init; }

    /// <summary>
    /// Request creation date (no time component).
    /// </summary>
    public required DateOnly CreatedAt { get; init; }
}
