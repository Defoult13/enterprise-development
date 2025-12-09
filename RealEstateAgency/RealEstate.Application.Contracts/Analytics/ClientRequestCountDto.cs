using RealEstate.Application.Contracts.Counterparties;

namespace RealEstate.Application.Contracts.Analytics;

/// <summary>
/// Client with number of requests.
/// </summary>
/// <param name="Client">Client data.</param>
/// <param name="Count">Number of requests for this client.</param>
public sealed record ClientRequestCountDto(
    CounterpartyDto Client,
    int Count
);