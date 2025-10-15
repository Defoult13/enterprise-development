using System.ComponentModel.DataAnnotations;

namespace RealEstate.Domain.Models;

/// <summary>
/// EF Core entity for a real estate object (table: Properties).
/// Includes technical characteristics and cadastral identity.
/// </summary>
public class Property
{
    /// <summary>Primary key (DB-generated).</summary>
    public required int Id { get; set; }

    /// <summary>Object type (apartment, house, office, etc.).</summary>
    public required PropertyType Type { get; set; }

    /// <summary>Functional purpose (residential or commercial).</summary>
    public required PropertyPurpose Purpose { get; set; }

    /// <summary>Cadastral (registry) number used by authorities.</summary>
    [Required, MaxLength(64)]
    public required string CadastralNumber { get; set; }

    /// <summary>Full postal address.</summary>
    [Required, MaxLength(256)]
    public required string Address { get; set; }

    /// <summary>Total number of storeys in the building (0 for land).</summary>
    public required int StoreysTotal { get; set; }

    /// <summary>Total area in square meters.</summary>
    public required double TotalArea { get; set; }

    /// <summary>Number of rooms (0 for land/warehouses if not applicable).</summary>
    public required int Rooms { get; set; }

    /// <summary>Ceiling height in meters (0 when not applicable).</summary>
    public required double CeilingHeight { get; set; }

    /// <summary>Floor on which the unit is located; null for houses/land.</summary>
    public int? FloorNumber { get; set; }

    /// <summary>Indicates if there are any encumbrances (liens, arrests, etc.).</summary>
    public required bool HasEncumbrances { get; set; }

    /// <summary>Navigation: applications related to this property.</summary>
    public ICollection<Application> Applications { get; set; } = new List<Application>();
}
