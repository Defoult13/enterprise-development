namespace RealEstateAgency.Domain.Models;

/// <summary>
/// A client (counterparty) of the real estate agency.
/// </summary>
public sealed class Counterparty
{
    /// <summary>
    /// Integer identifier assigned explicitly in seed/data layer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Full name of the client.
    /// </summary>
    public required string FullName { get; init; }

    /// <summary>
    /// Passport/ID number of the client.
    /// </summary>
    public required string PassportNumber { get; init; }

    /// <summary>
    /// Contact phone number (e.g., +7-XXX-XXX-XX-XX).
    /// </summary>
    public required string Phone { get; init; }
}
