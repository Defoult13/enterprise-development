using RealEstate.Domain.DataSeeder;
using RealEstate.Domain.Models;

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
            .Select(r => r.Client.Id)
            .Distinct()
            .OrderBy(id => id)
            .ToList();

        Assert.Equal(new[] { 1, 2, 3, 4 }, sellerIds);
    }

    [Fact(DisplayName = "Top-5 clients by request type (combined payload, IDs + Name + Count)")]
    public void GetTopClientsByType_WhenGrouped_ReturnsCombinedTop5ForBuyAndSell()
    {
        var groupedTop = data.Requests
            .GroupBy(r => r.Type)
            .Select(g => new
            {
                Type = g.Key.ToString()!.ToLower(),
                Clients = g.GroupBy(r => r.Client.Id)
                           .Select(cg => new
                           {
                               ClientId = cg.Key,
                               Name = cg.First().Client.FullName,
                               Count = cg.Count()
                           })
                           .OrderByDescending(x => x.Count)
                           .ThenBy(x => x.Name)
                           .Take(5)
                           .ToList()
            })
            .OrderBy(x => x.Type)
            .ToList();

        var result = new { items = groupedTop };

        Assert.Equal(2, result.items.Count);
        Assert.Contains(result.items, g => g.Type == "sell");
        Assert.Contains(result.items, g => g.Type == "buy");
        Assert.All(result.items, g => Assert.True(g.Clients.Count <= 5));

        var sell = result.items.Single(x => x.Type == "sell").Clients
            .ToDictionary(c => c.ClientId, c => c.Count);
        Assert.Equal(2, sell[1]);
        Assert.Equal(2, sell[2]);
        Assert.Equal(2, sell[3]);

        var buy = result.items.Single(x => x.Type == "buy").Clients
            .ToDictionary(c => c.ClientId, c => c.Count);
        Assert.Equal(3, buy[5]);
    }

    [Fact(DisplayName = "Buyers for a given property type: return IDs, order by name for presentation")]
    public void GetBuyersByPropertyType_WhenApartment_ReturnsIdsOrderedByFullName()
    {
        var targetType = PropertyType.Apartment;

        var buyers = data.Requests
            .Where(r => r.Type == RequestType.Buy && r.Property.Type == targetType)
            .GroupBy(r => r.Client.Id)
            .Select(g => new { ClientId = g.Key, Name = g.First().Client.FullName })
            .OrderBy(x => x.Name)
            .ToList();

        var expectedIds = new[] { 5, 6, 8 };
        Assert.True(expectedIds.OrderBy(i => i).SequenceEqual(buyers.Select(b => b.ClientId).OrderBy(i => i)));
        Assert.True(buyers.Select(b => b.Name).SequenceEqual(buyers.Select(b => b.Name).OrderBy(n => n)));
    }

    [Fact(DisplayName = "Request counts by property type")]
    public void GetRequestCounts_WhenGroupedByPropertyType_ReturnsExpectedTotals()
    {
        var byType = data.Requests
            .GroupBy(r => r.Property.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(7, byType[PropertyType.Apartment]);
        Assert.Equal(3, byType[PropertyType.House]);
        Assert.Equal(2, byType[PropertyType.Office]);
        Assert.Equal(2, byType[PropertyType.Land]);
        Assert.Equal(1, byType[PropertyType.Warehouse]);
        Assert.Equal(1, byType[PropertyType.Retail]);
    }

    [Fact(DisplayName = "Clients with minimal request amount (IDs, alphabetical presentation)")]
    public void GetClientsWithMinimumAmount_WhenCalculated_ReturnsIdsAndAlphabeticalNames()
    {
        var min = data.Requests.Min(r => r.Amount);

        var clients = data.Requests
            .Where(r => r.Amount == min)
            .Select(r => new { r.Client.Id, r.Client.FullName })
            .Distinct()
            .OrderBy(x => x.FullName)
            .ToList();

        Assert.Equal(1_000_000m, min);
        Assert.Equal(2, clients.Count);
        Assert.Equal(new[] { 6, 8 }, clients.Select(c => c.Id).OrderBy(i => i).ToArray());
        Assert.Equal(new[] { "Соколова Света", "Фёдорова Фаина" }, clients.Select(c => c.FullName).ToArray());
    }
}
