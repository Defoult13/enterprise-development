using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Application.Contracts.RealEstateRequests;

/// <summary>
/// DTO for getting request data.
/// </summary>
/// <param name="Id">Request id.</param>
/// <param name="ClientId">Related counterparty id.</param>
/// <param name="PropertyId">Related real-estate object id.</param>
/// <param name="Type">Request type (buy/sell).</param>
/// <param name="Amount">Requested amount.</param>
/// <param name="CreatedAt">Creation date.</param>
public sealed record RealEstateRequestDto(
    int Id,
    int ClientId,
    int PropertyId,
    RequestType Type,
    decimal Amount,
    DateOnly CreatedAt
);