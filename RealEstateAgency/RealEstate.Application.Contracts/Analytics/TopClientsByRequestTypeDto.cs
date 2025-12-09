using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Application.Contracts.Analytics;

/// <summary>
/// Top clients for a specific request type.
/// </summary>
/// <param name="Type">Request type (buy/sell).</param>
/// <param name="Clients">Clients with request counts.</param>
public sealed record TopClientsByRequestTypeDto(
    RequestType Type,
    IList<ClientRequestCountDto> Clients
);