namespace RealEstate.Domain.Models;

/// <summary>
/// EF Core entity describing an intent to buy or sell a specific property.
/// </summary>
public class Application
{
    /// <summary>Primary key (DB-generated).</summary>
    public required int Id { get; set; }

    /// <summary>FK to the client who submitted the application.</summary>
    public required int ClientId { get; set; }

    /// <summary>Navigation to client.</summary>
    public required Counterparty Client { get; set; }

    /// <summary>FK to the property related to this application.</summary>
    public required int PropertyId { get; set; }

    /// <summary>Navigation to property.</summary>
    public required Property Property { get; set; }

    /// <summary>Whether client intends to buy or sell.</summary>
    public required ApplicationType Type { get; set; }

    /// <summary>Requested or proposed amount in local currency.</summary>
    public required decimal Amount { get; set; }

    /// <summary>Date when the application was created (no time part).</summary>
    public required DateOnly DateCreated { get; set; }
}
