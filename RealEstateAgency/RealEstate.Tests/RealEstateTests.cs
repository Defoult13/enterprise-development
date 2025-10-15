using RealEstate.Domain.Fixtures;
using RealEstate.Domain.Models;

/// <summary>
/// LINQ-based unit tests verifying analytical queries over in-memory EF-style entities.
/// Uses DateOnly (DateCreated) and alphabetical tie-breakers when counts are equal.
/// </summary>
public class RealEstateTests
{
    private readonly RealEstateFixture _fx = new();

    /// <summary>
    /// All unique sellers who submitted applications within a given period.
    /// </summary>
    [Fact]
    public void Sellers_In_Period()
    {
        var from = new DateOnly(2024, 06, 01);
        var to = new DateOnly(2024, 07, 31).AddDays(1);

        var sellers = _fx.Applications
            .Where(a => a.Type == ApplicationType.Sell
                     && a.DateCreated >= from && a.DateCreated < to)
            .Select(a => a.Client.FullName)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        Assert.Equal(new[]
        {
            "Антонова Анна",   // 2024-06-05
            "Иванов Иван",     // 2024-06-15
            "Петров Пётр",     // 2024-07-20
            "Сидоров Степан"   // 2024-07-15
        }, sellers);
    }

    /// <summary>
    /// Top-5 clients by number of applications (separately for Buy and Sell).
    /// </summary>
    [Fact]
    public void Top5_By_Count_Buy_And_Sell()
    {
        var buysTop = _fx.Applications
            .Where(a => a.Type == ApplicationType.Buy)
            .GroupBy(a => a.Client.FullName)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count).ThenBy(x => x.Name)
            .Take(5).ToList();

        var sellsTop = _fx.Applications
            .Where(a => a.Type == ApplicationType.Sell)
            .GroupBy(a => a.Client.FullName)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count).ThenBy(x => x.Name)
            .Take(5).ToList();

        
        Assert.Equal(("Кузнецов Кирилл", 3), (buysTop[0].Name, buysTop[0].Count));
        Assert.Equal(("Соколова Света", 2), (buysTop[1].Name, buysTop[1].Count));
        Assert.Equal(("Фёдорова Фаина", 2), (buysTop[2].Name, buysTop[2].Count));

        
        Assert.Equal(("Иванов Иван", 2), (sellsTop[0].Name, sellsTop[0].Count));
        Assert.Equal(("Петров Пётр", 2), (sellsTop[1].Name, sellsTop[1].Count));
        Assert.Equal(("Сидоров Степан", 2), (sellsTop[2].Name, sellsTop[2].Count));
    }

    /// <summary>
    /// Number of applications per property type.
    /// </summary>
    [Fact]
    public void Count_By_PropertyType()
    {
        var byType = _fx.Applications
            .GroupBy(a => a.Property.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        Assert.Equal(7, byType[PropertyType.Apartment]);
        Assert.Equal(3, byType[PropertyType.House]);
        Assert.Equal(2, byType[PropertyType.Office]);
        Assert.Equal(2, byType[PropertyType.Land]);
        Assert.Equal(1, byType[PropertyType.Warehouse]);
        Assert.Equal(1, byType[PropertyType.Retail]);
    }

    /// <summary>
    /// Clients with minimal application amount.
    /// </summary>
    [Fact]
    public void Clients_With_Min_Amount()
    {
        var min = _fx.Applications.Min(a => a.Amount);

        var clients = _fx.Applications
            .Where(a => a.Amount == min)
            .Select(a => a.Client.FullName)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        Assert.Equal(new[] { "Соколова Света", "Фёдорова Фаина" }, clients);
        Assert.Equal(1_000_000m, min);
    }

    /// <summary>
    /// Clients searching for a given property type (e.g., Apartment), ordered by name.
    /// </summary>
    [Fact]
    public void Buyers_Searching_For_Given_Type_Sorted()
    {
        var type = PropertyType.Apartment;

        var buyers = _fx.Applications
            .Where(a => a.Type == ApplicationType.Buy && a.Property.Type == type)
            .Select(a => a.Client.FullName)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        Assert.Equal(new[] { "Кузнецов Кирилл", "Соколова Света", "Фёдорова Фаина" }, buyers);
    }
}
