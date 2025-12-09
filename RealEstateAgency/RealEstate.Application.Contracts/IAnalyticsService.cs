using RealEstate.Application.Contracts.Analytics;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Application.Contracts;

/// <summary>
/// Analytics service contract for aggregated, read-only queries over requests, clients and properties.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Returns all sellers who created SELL requests within the given period.
    /// </summary>
    /// <param name="from">Start date.</param>
    /// <param name="to">End date (exclusive).</param>
    public Task<IList<CounterpartyDto>> GetSellersByPeriod(DateOnly from, DateOnly to);

    /// <summary>
    /// Returns top 5 clients by number of requests, separately for BUY and SELL.
    /// </summary>
    public Task<IList<TopClientsByRequestTypeDto>> GetTop5ClientsByRequestType();

    /// <summary>
    /// Returns counts of requests grouped by property type.
    /// </summary>
    public Task<IList<RequestCountByPropertyTypeDto>> GetRequestCountsByPropertyType();

    /// <summary>
    /// Returns all clients who created requests with the minimal amount across all requests.
    /// </summary>
    public Task<IList<CounterpartyDto>> GetClientsWithMinimumRequestAmount();

    /// <summary>
    /// Returns all clients who are looking to BUY a property of the given type,
    /// ordered by full name.
    /// </summary>
    /// <param name="propertyType">Target property type.</param>
    public Task<IList<CounterpartyDto>> GetBuyersByPropertyType(PropertyType propertyType);
}