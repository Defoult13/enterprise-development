namespace RealEstateAgency.Domain.Models;

/// <summary>
/// A client (counterparty) of the real estate agency.
/// </summary>
public sealed class Counterparty
{
    /// <summary>
    /// Unique identifier of the client.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

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
