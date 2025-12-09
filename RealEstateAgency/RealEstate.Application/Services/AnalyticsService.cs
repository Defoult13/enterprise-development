using AutoMapper;
using RealEstate.Application.Contracts;
using RealEstate.Application.Contracts.Analytics;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Domain;
using RealEstate.Domain.Models;
using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Application.Services;

/// <summary>
/// Service that provides aggregated, read-only analytics queries over requests, clients and properties.
/// </summary>
/// <param name="requestRepo">Repository for accessing requests.</param>
/// <param name="counterpartyRepo">Repository for accessing counterparties.</param>
/// <param name="realEstateObjectRepo">Repository for accessing real-estate objects.</param>
/// <param name="mapper">Mapper used to convert entities to DTOs.</param>
public class AnalyticsService(
    IRepository<RealEstateRequest, int> requestRepo,
    IRepository<Counterparty, int> counterpartyRepo,
    IRepository<RealEstateObject, int> realEstateObjectRepo,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Returns all sellers who created SELL requests within the given period.
    /// </summary>
    /// <param name="from">Start date.</param>
    /// <param name="to">End date (exclusive).</param>
    /// <returns>Distinct sellers as counterparty DTOs.</returns>
    public async Task<IList<CounterpartyDto>> GetSellersByPeriod(DateOnly from, DateOnly to)
    {
        var requests = await requestRepo.GetAll();
        var counterparties = await counterpartyRepo.GetAll();

        var sellerIds = requests
            .Where(r => r.Type == RequestType.Sell && r.CreatedAt >= from && r.CreatedAt < to)
            .Select(r => r.ClientId)
            .Distinct()
            .ToHashSet();

        var sellers = counterparties
            .Where(c => sellerIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .ToList();

        return [.. sellers.Select(mapper.Map<CounterpartyDto>)];
    }

    /// <summary>
    /// Returns top 5 clients by number of requests, separately for BUY and SELL.
    /// </summary>
    /// <returns>Top clients grouped by request type with counts.</returns>
    public async Task<IList<TopClientsByRequestTypeDto>> GetTop5ClientsByRequestType()
    {
        var requests = await requestRepo.GetAll();
        var counterparties = await counterpartyRepo.GetAll();

        var clientsById = counterparties.ToDictionary(c => c.Id);

        var result = requests
            .GroupBy(r => r.Type)
            .Select(g => new TopClientsByRequestTypeDto(
                g.Key,
                [.. g.GroupBy(r => r.ClientId)
                  .Select(cg => new { ClientId = cg.Key, Count = cg.Count() })
                  .OrderByDescending(x => x.Count)
                  .ThenBy(x => clientsById[x.ClientId].FullName)
                  .Take(5)
                  .Select(x => new ClientRequestCountDto(
                      mapper.Map<CounterpartyDto>(clientsById[x.ClientId]),
                      x.Count
                  ))]
            ))
            .OrderBy(x => x.Type)
            .ToList();

        return result;
    }


    /// <summary>
    /// Returns counts of requests grouped by property type.
    /// </summary>
    /// <returns>List of property types with request counts.</returns>
    public async Task<IList<RequestCountByPropertyTypeDto>> GetRequestCountsByPropertyType()
    {
        var requests = await requestRepo.GetAll();
        var properties = await realEstateObjectRepo.GetAll();

        var propertyTypeById = properties.ToDictionary(p => p.Id, p => p.Type);

        var counts = requests
            .GroupBy(r => propertyTypeById[r.PropertyId])
            .Select(g => new RequestCountByPropertyTypeDto(g.Key, g.Count()))
            .OrderBy(x => x.PropertyType)
            .ToList();

        return counts;
    }

    /// <summary>
    /// Returns all clients who created requests with the minimal amount across all requests.
    /// </summary>
    /// <returns>Distinct clients as counterparty DTOs.</returns>
    public async Task<IList<CounterpartyDto>> GetClientsWithMinimumRequestAmount()
    {
        var requests = await requestRepo.GetAll();
        var counterparties = await counterpartyRepo.GetAll();

        if (requests.Count == 0)
            return [];

        var minAmount = requests.Min(r => r.Amount);

        var clientIds = requests
            .Where(r => r.Amount == minAmount)
            .Select(r => r.ClientId)
            .Distinct()
            .ToHashSet();

        var clients = counterparties
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .ToList();

        return [.. clients.Select(mapper.Map<CounterpartyDto>)];
    }

    /// <summary>
    /// Returns all clients who are looking to BUY a property of the given type,
    /// ordered by full name.
    /// </summary>
    /// <param name="propertyType">Target property type.</param>
    /// <returns>Distinct buyers as counterparty DTOs.</returns>
    public async Task<IList<CounterpartyDto>> GetBuyersByPropertyType(PropertyType propertyType)
    {
        var requests = await requestRepo.GetAll();
        var counterparties = await counterpartyRepo.GetAll();
        var properties = await realEstateObjectRepo.GetAll();

        var propertyTypeById = properties.ToDictionary(p => p.Id, p => p.Type);

        var buyerIds = requests
            .Where(r => r.Type == RequestType.Buy && propertyTypeById[r.PropertyId] == propertyType)
            .Select(r => r.ClientId)
            .Distinct()
            .ToHashSet();

        var buyers = counterparties
            .Where(c => buyerIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .ToList();

        return [.. buyers.Select(mapper.Map<CounterpartyDto>)];
    }
}