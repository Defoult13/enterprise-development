using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models;

/// <summary>
/// EF Core entity representing a client (individual/company).
/// </summary>
public class Counterparty
{
    /// <summary>Primary key (DB-generated).</summary>
    public required int Id { get; set; }

    /// <summary>Full name (for an individual) or legal name (for an organization).</summary>
    [Required, MaxLength(128)]
    public required string FullName { get; set; }

    /// <summary>Passport/ID number used for identification.</summary>
    [Required, MaxLength(32)]
    public required string PassportNumber { get; set; }

    /// <summary>Primary contact phone number.</summary>
    [Required, MaxLength(32)]
    public required string Phone { get; set; }

    /// <summary>Navigation: applications submitted by the client.</summary>
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
