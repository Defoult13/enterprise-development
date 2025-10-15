using RealEstate.Domain.Models;

namespace RealEstate.Domain.Fixtures;

/// <summary>
/// In-memory seed data for tests or demos (EF-friendly):
/// int keys, FK fields, navigation wiring, and required initializers.
/// </summary>
public sealed class RealEstateFixture
{
    public IReadOnlyList<Counterparty> Counterparties { get; }
    public IReadOnlyList<Property> Properties { get; }
    public IReadOnlyList<Application> Applications { get; }

    public RealEstateFixture()
    {
        // --- Clients ---
        var c = new List<Counterparty>
        {
            NewClient(1, "Иванов Иван",    "4010 111111", "+7-900-000-01-01"),
            NewClient(2, "Петров Пётр",    "4010 222222", "+7-900-000-02-02"),
            NewClient(3, "Сидоров Степан", "4010 333333", "+7-900-000-03-03"),
            NewClient(4, "Антонова Анна",  "4010 444444", "+7-900-000-04-04"),
            NewClient(5, "Кузнецов Кирилл","4010 555555", "+7-900-000-05-05"),
            NewClient(6, "Соколова Света", "4010 666666", "+7-900-000-06-06"),
            NewClient(7, "Романов Роман",  "4010 777777", "+7-900-000-07-07"),
            NewClient(8, "Фёдорова Фаина", "4010 888888", "+7-900-000-08-08"),
            NewClient(9, "Морозов Максим", "4010 999999", "+7-900-000-09-09"),
            NewClient(10,"Ким Денис",      "4010 101010", "+7-900-000-10-10"),
            NewClient(11,"Осипова Олеся",  "4010 111112", "+7-900-000-11-11"),
            NewClient(12,"Громов Григорий","4010 121212", "+7-900-000-12-12"),
        };
        Counterparties = c;

        // --- Properties ---
        var p = new List<Property>
        {
            NewProp(1, PropertyType.Apartment, PropertyPurpose.Residential, "77:01:0001001:1", "Москва, ул. Первая, 1", 17, 52.3, 2, 2.7, 7, false),
            NewProp(2, PropertyType.House,     PropertyPurpose.Residential, "50:01:0002002:2", "МО, Мытищи, ул. Лесная, 3", 2, 180, 5, 3.0, null, false),
            NewProp(3, PropertyType.Office,    PropertyPurpose.Commercial,  "78:01:0003003:3", "СПб, Невский 10", 8, 95, 4, 3.2, 3, true),
            NewProp(4, PropertyType.Land,      PropertyPurpose.Residential, "23:01:0004004:4", "Краснодарский край, уч. 45", 0, 10_00, 0, 0, null, false),
            NewProp(5, PropertyType.Warehouse, PropertyPurpose.Commercial,  "66:01:0005005:5", "Екатеринбург, Промзона 12", 1, 450, 1, 6, 1, true),
            NewProp(6, PropertyType.Apartment, PropertyPurpose.Residential, "77:01:0006006:6", "Москва, ул. Вторая, 5", 25, 40, 1, 2.6, 16, false),
            NewProp(7, PropertyType.Apartment, PropertyPurpose.Residential, "77:01:0007007:7", "Москва, ул. Третья, 7", 25, 36, 1, 2.6, 12, false),
            NewProp(8, PropertyType.House,     PropertyPurpose.Residential, "50:01:0008008:8", "МО, Балашиха, Заречная 8", 2, 140, 4, 2.9, null, false),
            NewProp(9, PropertyType.Office,    PropertyPurpose.Commercial,  "78:01:0009009:9", "СПб, Сенная, 2", 12, 120, 5, 3.3, 6, false),
            NewProp(10, PropertyType.Apartment, PropertyPurpose.Residential, "77:01:0010010:0", "Москва, ул. Новая, 9", 22, 58, 3, 2.8, 9, false),
            NewProp(11, PropertyType.Land,     PropertyPurpose.Residential, "23:01:0011011:1", "Краснодарский край, уч. 17", 0, 8_00, 0, 0, null, false),
            NewProp(12, PropertyType.Retail,   PropertyPurpose.Commercial,  "66:01:0012012:2", "Екатеринбург, ТЦ «Океан»", 3, 75, 2, 3.4, 1, false),
        };
        Properties = p;

        // --- Applications ---
        var apps = new List<Application>
        {
            NewApp(1, c[0], p[0], ApplicationType.Sell, 5_000_000m,  new DateOnly(2024,06,15)),
            NewApp(2, c[1], p[1], ApplicationType.Sell,10_000_000m,  new DateOnly(2024,07,20)),
            NewApp(3, c[2], p[2], ApplicationType.Sell,15_000_000m,  new DateOnly(2024,05,10)),
            NewApp(4, c[3], p[3], ApplicationType.Sell, 2_000_000m,  new DateOnly(2024,06,05)),
            NewApp(5, c[0], p[4], ApplicationType.Sell, 3_000_000m,  new DateOnly(2023,01,01)), // Иванов = 2 продажи
            NewApp(6, c[4], p[5], ApplicationType.Buy,  3_500_000m,  new DateOnly(2024,06,18)),
            NewApp(7, c[5], p[6], ApplicationType.Buy,  1_000_000m,  new DateOnly(2024,06,19)),
            NewApp(8, c[6], p[7], ApplicationType.Buy,  5_000_000m,  new DateOnly(2024,07,01)),
            NewApp(9, c[5], p[8], ApplicationType.Buy,  2_000_000m,  new DateOnly(2024,07,02)),
            NewApp(10, c[7], p[9], ApplicationType.Buy,  1_000_000m,  new DateOnly(2024,07,03)),
            NewApp(11, c[7], p[10],ApplicationType.Buy,  1_200_000m,  new DateOnly(2024,07,04)),
            NewApp(12, c[8], p[11],ApplicationType.Buy,  2_200_000m,  new DateOnly(2024,07,05)),
            NewApp(13, c[4], p[0], ApplicationType.Buy,  4_100_000m,  new DateOnly(2024,08,01)),
            NewApp(14, c[4], p[1], ApplicationType.Buy,  4_200_000m,  new DateOnly(2024,08,02)),
            NewApp(15, c[2], p[5], ApplicationType.Sell, 7_000_000m,  new DateOnly(2024,07,15)),
            NewApp(16, c[1], p[6], ApplicationType.Sell,12_000_000m,  new DateOnly(2024,08,10)),
        };

        foreach (var a in apps)
        {
            a.Client.Applications.Add(a);
            a.Property.Applications.Add(a);
        }

        Applications = apps;
    }

    private static Counterparty NewClient(int id, string name, string pass, string phone) =>
        new() { Id = id, FullName = name, PassportNumber = pass, Phone = phone };

    private static Property NewProp(
        int id, PropertyType type, PropertyPurpose purpose, string cad, string address,
        int storeys, double area, int rooms, double ceil, int? floor, bool hasEnc) =>
        new()
        {
            Id = id,
            Type = type,
            Purpose = purpose,
            CadastralNumber = cad,
            Address = address,
            StoreysTotal = storeys,
            TotalArea = area,
            Rooms = rooms,
            CeilingHeight = ceil,
            FloorNumber = floor,
            HasEncumbrances = hasEnc
        };

    private static Application NewApp(
        int id, Counterparty client, Property property,
        ApplicationType t, decimal amount, DateOnly date) =>
        new()
        {
            Id = id,
            ClientId = client.Id,
            Client = client,
            PropertyId = property.Id,
            Property = property,
            Type = t,
            Amount = amount,
            DateCreated = date
        };
}
