using RealEstate.Domain.DataSeeder;
using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Tests;

/// <summary>
/// Unit tests verifying analytical queries over in-memory real estate data.
/// </summary>
public sealed class RealEstateQueries_Fixed(RealEstateDataSeeder data)
    : IClassFixture<RealEstateDataSeeder>
{
    [Fact(DisplayName = "Sellers in period: return IDs (half-open [from, to))")]
    public void GetSellersByPeriod_WhenRangeGiven_ReturnsDistinctSellerIds()
    {
        var from = new DateOnly(2024, 06, 01);
        var to = new DateOnly(2024, 08, 01);

        var sellerIds = data.Requests
            .Where(r => r.Type == RequestType.Sell
                     && r.CreatedAt >= from && r.CreatedAt < to)
            .Select(r => r.ClientId)
            .Distinct()
            .ToList();

        Assert.Equal(4, sellerIds.Count);
        Assert.Contains(1, sellerIds);
    }

    [Fact(DisplayName = "Top-5 clients by request type (combined payload, IDs + Name + Count)")]
    public void GetTopClientsByType_WhenGrouped_ReturnsCombinedTop5ForBuyAndSell()
    {
        var clientsById = data.Counterparties.ToDictionary(c => c.Id, c => c.FullName);

        var groupedTop = data.Requests
            .GroupBy(r => r.Type)
            .Select(g => new
            {
                Type = g.Key.ToString()!.ToLowerInvariant(),
                Clients = g.GroupBy(r => r.ClientId)
                           .Select(cg => new
                           {
                               ClientId = cg.Key,
                               Name = clientsById[cg.Key],
                               Count = cg.Count()
                           })
                           .OrderByDescending(x => x.Count)
                           .ThenBy(x => x.Name)
                           .Take(5)
                           .ToList()
            })
            .ToList();

        Assert.Equal(2, groupedTop.Count);
        Assert.Contains(groupedTop, g => g.Type == "sell");
        Assert.Contains(groupedTop, g => g.Type == "buy");

        var sellTop = groupedTop.Single(x => x.Type == "sell").Clients;
        Assert.True(sellTop.Count <= 5);
        Assert.Contains(sellTop, c => c.ClientId == 1 && c.Count == 2);

        var buyTop = groupedTop.Single(x => x.Type == "buy").Clients;
        Assert.True(buyTop.Count <= 5);
        Assert.Contains(buyTop, c => c.ClientId == 5 && c.Count == 3);
    }

    [Fact(DisplayName = "Buyers for a given property type: return IDs, order by name for presentation")]
    public void GetBuyersByPropertyType_WhenApartment_ReturnsIdsOrderedByFullName()
    {
        var targetType = PropertyType.Apartment;

        var clientsById = data.Counterparties.ToDictionary(c => c.Id, c => c.FullName);
        var propertiesById = data.Properties.ToDictionary(p => p.Id, p => p.Type);

        var buyers = data.Requests
            .Where(r => r.Type == RequestType.Buy && propertiesById[r.PropertyId] == targetType)
            .GroupBy(r => r.ClientId)
            .Select(g => new { ClientId = g.Key, Name = clientsById[g.Key] })
            .OrderBy(x => x.Name)
            .ToList();

        Assert.Equal(3, buyers.Count);
        Assert.Contains(buyers, b => b.ClientId == 6);
    }

    [Fact(DisplayName = "Request counts by property type")]
    public void GetRequestCounts_WhenGroupedByPropertyType_ReturnsExpectedTotals()
    {
        var propertiesById = data.Properties.ToDictionary(p => p.Id, p => p.Type);

        var byType = data.Requests
            .GroupBy(r => propertiesById[r.PropertyId])
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(6, byType.Count);
        Assert.True(byType.ContainsKey(PropertyType.Apartment));
        Assert.Equal(7, byType[PropertyType.Apartment]);
    }

    [Fact(DisplayName = "Clients with minimal request amount (IDs, alphabetical presentation)")]
    public void GetClientsWithMinimumAmount_WhenCalculated_ReturnsIdsAndAlphabeticalNames()
    {
        var min = data.Requests.Min(r => r.Amount);

        var clientsById = data.Counterparties.ToDictionary(c => c.Id, c => c.FullName);

        var clients = data.Requests
            .Where(r => r.Amount == min)
            .Select(r => new { Id = r.ClientId, FullName = clientsById[r.ClientId] })
            .Distinct()
            .OrderBy(x => x.FullName)
            .ToList();

        Assert.Equal(1_000_000m, min);
        Assert.Equal(2, clients.Count);
        Assert.Contains(clients, c => c.Id == 8);
    }
}
