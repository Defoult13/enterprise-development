using RealEstate.Domain.Models;
using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Domain.DataSeeder;

/// <summary>
/// Provides in-memory seed data (clients, properties, requests) for unit tests and demos.
/// </summary>
public sealed class RealEstateDataSeeder
{
    /// <summary>
    /// Seeded clients.
    /// </summary>
    public List<Counterparty> Counterparties { get; } =
    [
        new Counterparty { Id = 1, FullName = "Иванов Иван", PassportNumber = "4010 111111", Phone = "+7-900-000-01-01" },
        new Counterparty { Id = 2, FullName = "Петров Пётр", PassportNumber = "4010 222222", Phone = "+7-900-000-02-02" },
        new Counterparty { Id = 3, FullName = "Сидоров Степан", PassportNumber = "4010 333333", Phone = "+7-900-000-03-03" },
        new Counterparty { Id = 4, FullName = "Антонова Анна", PassportNumber = "4010 444444", Phone = "+7-900-000-04-04" },
        new Counterparty { Id = 5, FullName = "Кузнецов Кирилл", PassportNumber = "4010 555555", Phone = "+7-900-000-05-05" },
        new Counterparty { Id = 6, FullName = "Соколова Света", PassportNumber = "4010 666666", Phone = "+7-900-000-06-06" },
        new Counterparty { Id = 7, FullName = "Романов Роман", PassportNumber = "4010 777777", Phone = "+7-900-000-07-07" },
        new Counterparty { Id = 8, FullName = "Фёдорова Фаина", PassportNumber = "4010 888888", Phone = "+7-900-000-08-08" },
        new Counterparty { Id = 9, FullName = "Морозов Максим", PassportNumber = "4010 999999", Phone = "+7-900-000-09-09" },
        new Counterparty { Id = 10, FullName = "Ким Денис", PassportNumber = "4010 101010", Phone = "+7-900-000-10-10" },
        new Counterparty { Id = 11, FullName = "Осипова Олеся", PassportNumber = "4010 111112", Phone = "+7-900-000-11-11" },
        new Counterparty { Id = 12, FullName = "Громов Григорий", PassportNumber = "4010 121212", Phone = "+7-900-000-12-12" },
    ];

    /// <summary>
    /// Seeded real-estate objects.
    /// </summary>
    public List<RealEstateObject> Properties { get; } =
    [
        new RealEstateObject
        {
            Id = 1,
            Type = PropertyType.Apartment,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "77:01:0001001:1",
            Address = "Москва, ул. Первая, 1",
            FloorsTotal = 17,
            TotalAreaSqM = 52.3,
            Rooms = 2,
            CeilingHeightM = 2.7,
            Floor = 7,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 2,
            Type = PropertyType.House,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "50:01:0002002:2",
            Address = "МО, Мытищи, ул. Лесная, 3",
            FloorsTotal = 2,  TotalAreaSqM = 180,
            Rooms = 5,
            CeilingHeightM = 3.0,
            Floor = null,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 3,
            Type = PropertyType.Office,
            Purpose = PropertyPurpose.Commercial,
            CadastralNumber = "78:01:0003003:3",
            Address = "СПб, Невский 10",
            FloorsTotal = 8,
            TotalAreaSqM = 95,
            Rooms = 4,
            CeilingHeightM = 3.2,
            Floor = 3,
            HasEncumbrances = true
        },
        new RealEstateObject
        {
            Id = 4,
            Type = PropertyType.Land,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "23:01:0004004:4",
            Address = "Краснодарский край, уч. 45",
            FloorsTotal = 0,
            TotalAreaSqM = 1000,
            Rooms = 0,
            CeilingHeightM = 0,
            Floor = null,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 5,
            Type = PropertyType.Warehouse,
            Purpose = PropertyPurpose.Commercial,
            CadastralNumber = "66:01:0005005:5",
            Address = "Екатеринбург, Промзона 12",
            FloorsTotal = 1,
            TotalAreaSqM = 450,
            Rooms = 1,
            CeilingHeightM = 6,
            Floor = 1,
            HasEncumbrances = true
        },
        new RealEstateObject
        {
            Id = 6,
            Type = PropertyType.Apartment,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "77:01:0006006:6",
            Address = "Москва, ул. Вторая, 5",
            FloorsTotal = 25,
            TotalAreaSqM = 40,
            Rooms = 1,
            CeilingHeightM = 2.6,
            Floor = 16,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 7,
            Type = PropertyType.Apartment,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "77:01:0007007:7",
            Address = "Москва, ул. Третья, 7",
            FloorsTotal = 25,
            TotalAreaSqM = 36,
            Rooms = 1,
            CeilingHeightM = 2.6,
            Floor = 12,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 8,
            Type = PropertyType.House,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "50:01:0008008:8",
            Address = "МО, Балашиха, Заречная 8",
            FloorsTotal = 2,
            TotalAreaSqM = 140,
            Rooms = 4,
            CeilingHeightM = 2.9,
            Floor = null,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 9,
            Type = PropertyType.Office,
            Purpose = PropertyPurpose.Commercial,
            CadastralNumber = "78:01:0009009:9",
            Address = "СПб, Сенная, 2",
            FloorsTotal = 12,
            TotalAreaSqM = 120,
            Rooms = 5,
            CeilingHeightM = 3.3,
            Floor = 6,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 10,
            Type = PropertyType.Apartment,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "77:01:0010010:0",
            Address = "Москва, ул. Новая, 9",
            FloorsTotal = 22,
            TotalAreaSqM = 58,
            Rooms = 3,
            CeilingHeightM = 2.8,
            Floor = 9,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 11,
            Type = PropertyType.Land,
            Purpose = PropertyPurpose.Residential,
            CadastralNumber = "23:01:0011011:1",
            Address = "Краснодарский край, уч. 17",
            FloorsTotal = 0,
            TotalAreaSqM = 800,
            Rooms = 0,
            CeilingHeightM = 0,
            Floor = null,
            HasEncumbrances = false
        },
        new RealEstateObject
        {
            Id = 12,
            Type = PropertyType.Retail,
            Purpose = PropertyPurpose.Commercial,
            CadastralNumber = "66:01:0012012:2",
            Address = "Екатеринбург, ТЦ «Океан»",
            FloorsTotal = 3,
            TotalAreaSqM = 75,
            Rooms = 2,
            CeilingHeightM = 3.4,
            Floor = 1,
            HasEncumbrances = false
        },
    ];

    /// <summary>
    /// Seeded client requests (buy/sell).
    /// </summary>
    public List<RealEstateRequest> Requests { get; }

    /// <summary>
    /// Initializes the seeder and builds dependent collections (Requests) using already created
    /// Counterparties and Properties.
    /// </summary>
    public RealEstateDataSeeder()
    {
        Requests = CreateRequests(Counterparties, Properties);
    }

    /// <summary>
    /// Creates a deterministic set of requests that reference existing clients and properties by index.
    /// </summary>
    /// <param name="clients">Seeded clients list.</param>
    /// <param name="props">Seeded properties list.</param>
    /// <returns>List of seeded buy/sell requests.</returns>
    private static List<RealEstateRequest> CreateRequests(List<Counterparty> clients, List<RealEstateObject> props) =>
    [
        new RealEstateRequest { Id = 1, ClientId = clients[0].Id, PropertyId = props[0].Id, Type = RequestType.Sell, Amount = 5_000_000m, CreatedAt = new DateOnly(2024, 06, 15) },
        new RealEstateRequest { Id = 2, ClientId = clients[1].Id, PropertyId = props[1].Id, Type = RequestType.Sell, Amount = 10_000_000m, CreatedAt = new DateOnly(2024, 07, 20) },
        new RealEstateRequest { Id = 3, ClientId = clients[2].Id, PropertyId = props[2].Id, Type = RequestType.Sell, Amount = 15_000_000m, CreatedAt = new DateOnly(2024, 05, 10) },
        new RealEstateRequest { Id = 4, ClientId = clients[3].Id, PropertyId = props[3].Id, Type = RequestType.Sell, Amount = 2_000_000m, CreatedAt = new DateOnly(2024, 06, 05) },
        new RealEstateRequest { Id = 5, ClientId = clients[0].Id, PropertyId = props[4].Id, Type = RequestType.Sell, Amount = 3_000_000m, CreatedAt = new DateOnly(2023, 01, 01) },
        new RealEstateRequest { Id = 6, ClientId = clients[4].Id, PropertyId = props[5].Id, Type = RequestType.Buy, Amount = 3_500_000m, CreatedAt = new DateOnly(2024, 06, 18) },
        new RealEstateRequest { Id = 7, ClientId = clients[5].Id, PropertyId = props[6].Id, Type = RequestType.Buy, Amount = 1_000_000m, CreatedAt = new DateOnly(2024, 06, 19) },
        new RealEstateRequest { Id = 8, ClientId = clients[6].Id, PropertyId = props[7].Id, Type = RequestType.Buy, Amount = 5_000_000m, CreatedAt = new DateOnly(2024, 07, 01) },
        new RealEstateRequest { Id = 9, ClientId = clients[5].Id, PropertyId = props[8].Id, Type = RequestType.Buy, Amount = 2_000_000m, CreatedAt = new DateOnly(2024, 07, 02) },
        new RealEstateRequest { Id = 10, ClientId = clients[7].Id, PropertyId = props[9].Id, Type = RequestType.Buy, Amount = 1_000_000m, CreatedAt = new DateOnly(2024, 07, 03) },
        new RealEstateRequest { Id = 11, ClientId = clients[7].Id, PropertyId = props[10].Id, Type = RequestType.Buy, Amount = 1_200_000m, CreatedAt = new DateOnly(2024, 07, 04) },
        new RealEstateRequest { Id = 12, ClientId = clients[8].Id, PropertyId = props[11].Id, Type = RequestType.Buy, Amount = 2_200_000m, CreatedAt = new DateOnly(2024, 07, 05) },
        new RealEstateRequest { Id = 13, ClientId = clients[4].Id, PropertyId = props[0].Id, Type = RequestType.Buy, Amount = 4_100_000m, CreatedAt = new DateOnly(2024, 08, 01) },
        new RealEstateRequest { Id = 14, ClientId = clients[4].Id, PropertyId = props[1].Id, Type = RequestType.Buy, Amount = 4_200_000m, CreatedAt = new DateOnly(2024, 08, 02) },
        new RealEstateRequest { Id = 15, ClientId = clients[2].Id, PropertyId = props[5].Id, Type = RequestType.Sell, Amount = 7_000_000m, CreatedAt = new DateOnly(2024, 07, 15) },
        new RealEstateRequest { Id = 16, ClientId = clients[1].Id, PropertyId = props[6].Id, Type = RequestType.Sell, Amount = 12_000_000m, CreatedAt = new DateOnly(2024, 08, 10) },
    ];
}
