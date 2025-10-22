using RealEstateAgency.Domain.Models;

namespace RealEstateAgency.Domain.DataSeeder;

/// <summary>
/// Provides in-memory seed data (clients, properties, requests) for unit tests and demos.
/// </summary>
public sealed class RealEstateDataSeeder
{
    /// <summary>
    /// Seeded clients.
    /// </summary>
    public List<Counterparty> Counterparties { get; }

    /// <summary>
    /// Seeded real-estate objects.
    /// </summary>
    public List<RealEstateObject> Properties { get; }

    /// <summary>
    /// Seeded client requests (buy/sell).
    /// </summary>
    public List<Request> Requests { get; }

    public RealEstateDataSeeder()
    {
        Counterparties = CreateCounterparties();
        Properties = CreateProperties();
        Requests = CreateRequests(Counterparties, Properties);
    }

    private static List<Counterparty> CreateCounterparties() => new()
    {
        new() { Id = 1,  FullName = "Иванов Иван",     PassportNumber = "4010 111111", Phone = "+7-900-000-01-01" },
        new() { Id = 2,  FullName = "Петров Пётр",     PassportNumber = "4010 222222", Phone = "+7-900-000-02-02" },
        new() { Id = 3,  FullName = "Сидоров Степан",  PassportNumber = "4010 333333", Phone = "+7-900-000-03-03" },
        new() { Id = 4,  FullName = "Антонова Анна",   PassportNumber = "4010 444444", Phone = "+7-900-000-04-04" },
        new() { Id = 5,  FullName = "Кузнецов Кирилл", PassportNumber = "4010 555555", Phone = "+7-900-000-05-05" },
        new() { Id = 6,  FullName = "Соколова Света",  PassportNumber = "4010 666666", Phone = "+7-900-000-06-06" },
        new() { Id = 7,  FullName = "Романов Роман",   PassportNumber = "4010 777777", Phone = "+7-900-000-07-07" },
        new() { Id = 8,  FullName = "Фёдорова Фаина",  PassportNumber = "4010 888888", Phone = "+7-900-000-08-08" },
        new() { Id = 9,  FullName = "Морозов Максим",  PassportNumber = "4010 999999", Phone = "+7-900-000-09-09" },
        new() { Id = 10, FullName = "Ким Денис",       PassportNumber = "4010 101010", Phone = "+7-900-000-10-10" },
        new() { Id = 11, FullName = "Осипова Олеся",   PassportNumber = "4010 111112", Phone = "+7-900-000-11-11" },
        new() { Id = 12, FullName = "Громов Григорий", PassportNumber = "4010 121212", Phone = "+7-900-000-12-12" },
    };

    private static List<RealEstateObject> CreateProperties() => new()
    {
        new() { Id = 1,  Type = PropertyType.Apartment, Purpose = PropertyPurpose.Residential, CadastralNumber = "77:01:0001001:1",  Address = "Москва, ул. Первая, 1",          FloorsTotal = 17, TotalAreaSqM = 52.3m, Rooms = 2, CeilingHeightM = 2.7m, Floor = 7,  HasEncumbrances = false },
        new() { Id = 2,  Type = PropertyType.House,     Purpose = PropertyPurpose.Residential, CadastralNumber = "50:01:0002002:2",  Address = "МО, Мытищи, ул. Лесная, 3",     FloorsTotal = 2,  TotalAreaSqM = 180m,  Rooms = 5, CeilingHeightM = 3.0m, Floor = null, HasEncumbrances = false },
        new() { Id = 3,  Type = PropertyType.Office,    Purpose = PropertyPurpose.Commercial,  CadastralNumber = "78:01:0003003:3",  Address = "СПб, Невский 10",               FloorsTotal = 8,  TotalAreaSqM = 95m,   Rooms = 4, CeilingHeightM = 3.2m, Floor = 3,  HasEncumbrances = true  },
        new() { Id = 4,  Type = PropertyType.Land,      Purpose = PropertyPurpose.Residential, CadastralNumber = "23:01:0004004:4",  Address = "Краснодарский край, уч. 45",    FloorsTotal = 0,  TotalAreaSqM = 1000m, Rooms = 0, CeilingHeightM = 0m,   Floor = null, HasEncumbrances = false },
        new() { Id = 5,  Type = PropertyType.Warehouse, Purpose = PropertyPurpose.Commercial,  CadastralNumber = "66:01:0005005:5",  Address = "Екатеринбург, Промзона 12",    FloorsTotal = 1,  TotalAreaSqM = 450m,  Rooms = 1, CeilingHeightM = 6m,   Floor = 1,  HasEncumbrances = true  },
        new() { Id = 6,  Type = PropertyType.Apartment, Purpose = PropertyPurpose.Residential, CadastralNumber = "77:01:0006006:6",  Address = "Москва, ул. Вторая, 5",         FloorsTotal = 25, TotalAreaSqM = 40m,   Rooms = 1, CeilingHeightM = 2.6m, Floor = 16, HasEncumbrances = false },
        new() { Id = 7,  Type = PropertyType.Apartment, Purpose = PropertyPurpose.Residential, CadastralNumber = "77:01:0007007:7",  Address = "Москва, ул. Третья, 7",         FloorsTotal = 25, TotalAreaSqM = 36m,   Rooms = 1, CeilingHeightM = 2.6m, Floor = 12, HasEncumbrances = false },
        new() { Id = 8,  Type = PropertyType.House,     Purpose = PropertyPurpose.Residential, CadastralNumber = "50:01:0008008:8",  Address = "МО, Балашиха, Заречная 8",     FloorsTotal = 2,  TotalAreaSqM = 140m,  Rooms = 4, CeilingHeightM = 2.9m, Floor = null, HasEncumbrances = false },
        new() { Id = 9,  Type = PropertyType.Office,    Purpose = PropertyPurpose.Commercial,  CadastralNumber = "78:01:0009009:9",  Address = "СПб, Сенная, 2",               FloorsTotal = 12, TotalAreaSqM = 120m,  Rooms = 5, CeilingHeightM = 3.3m, Floor = 6,  HasEncumbrances = false },
        new() { Id = 10, Type = PropertyType.Apartment, Purpose = PropertyPurpose.Residential, CadastralNumber = "77:01:0010010:0",  Address = "Москва, ул. Новая, 9",         FloorsTotal = 22, TotalAreaSqM = 58m,   Rooms = 3, CeilingHeightM = 2.8m, Floor = 9,  HasEncumbrances = false },
        new() { Id = 11, Type = PropertyType.Land,      Purpose = PropertyPurpose.Residential, CadastralNumber = "23:01:0011011:1",  Address = "Краснодарский край, уч. 17",   FloorsTotal = 0,  TotalAreaSqM = 800m,  Rooms = 0, CeilingHeightM = 0m,   Floor = null, HasEncumbrances = false },
        new() { Id = 12, Type = PropertyType.Retail,    Purpose = PropertyPurpose.Commercial,  CadastralNumber = "66:01:0012012:2",  Address = "Екатеринбург, ТЦ «Океан»",     FloorsTotal = 3,  TotalAreaSqM = 75m,   Rooms = 2, CeilingHeightM = 3.4m, Floor = 1,  HasEncumbrances = false },
    };

    private static List<Request> CreateRequests(List<Counterparty> clients, List<RealEstateObject> props) => new()
    {
        new() { Id = 1,  Client = clients[0],  Property = props[0],  Type = RequestType.Sell, Amount = 5_000_000m,  CreatedAt = new DateOnly(2024,06,15) },
        new() { Id = 2,  Client = clients[1],  Property = props[1],  Type = RequestType.Sell, Amount =10_000_000m,  CreatedAt = new DateOnly(2024,07,20) },
        new() { Id = 3,  Client = clients[2],  Property = props[2],  Type = RequestType.Sell, Amount =15_000_000m,  CreatedAt = new DateOnly(2024,05,10) },
        new() { Id = 4,  Client = clients[3],  Property = props[3],  Type = RequestType.Sell, Amount = 2_000_000m,  CreatedAt = new DateOnly(2024,06,05) },
        new() { Id = 5,  Client = clients[0],  Property = props[4],  Type = RequestType.Sell, Amount = 3_000_000m,  CreatedAt = new DateOnly(2023,01,01) },
        new() { Id = 6,  Client = clients[4],  Property = props[5],  Type = RequestType.Buy,  Amount = 3_500_000m,  CreatedAt = new DateOnly(2024,06,18) },
        new() { Id = 7,  Client = clients[5],  Property = props[6],  Type = RequestType.Buy,  Amount = 1_000_000m,  CreatedAt = new DateOnly(2024,06,19) },
        new() { Id = 8,  Client = clients[6],  Property = props[7],  Type = RequestType.Buy,  Amount = 5_000_000m,  CreatedAt = new DateOnly(2024,07,01) },
        new() { Id = 9,  Client = clients[5],  Property = props[8],  Type = RequestType.Buy,  Amount = 2_000_000m,  CreatedAt = new DateOnly(2024,07,02) },
        new() { Id = 10, Client = clients[7],  Property = props[9],  Type = RequestType.Buy,  Amount = 1_000_000m,  CreatedAt = new DateOnly(2024,07,03) },
        new() { Id = 11, Client = clients[7],  Property = props[10], Type = RequestType.Buy,  Amount = 1_200_000m,  CreatedAt = new DateOnly(2024,07,04) },
        new() { Id = 12, Client = clients[8],  Property = props[11], Type = RequestType.Buy,  Amount = 2_200_000m,  CreatedAt = new DateOnly(2024,07,05) },
        new() { Id = 13, Client = clients[4],  Property = props[0],  Type = RequestType.Buy,  Amount = 4_100_000m,  CreatedAt = new DateOnly(2024,08,01) },
        new() { Id = 14, Client = clients[4],  Property = props[1],  Type = RequestType.Buy,  Amount = 4_200_000m,  CreatedAt = new DateOnly(2024,08,02) },
        new() { Id = 15, Client = clients[2],  Property = props[5],  Type = RequestType.Sell, Amount = 7_000_000m,  CreatedAt = new DateOnly(2024,07,15) },
        new() { Id = 16, Client = clients[1],  Property = props[6],  Type = RequestType.Sell, Amount =12_000_000m,  CreatedAt = new DateOnly(2024,08,10) },
    };
}
