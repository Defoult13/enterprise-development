namespace RealEstate.Domain.Models;

/// <summary>
/// A real-estate object with cadastral identity and technical characteristics.
/// </summary>
public sealed class RealEstateObject
{
    /// <summary>
    /// Integer identifier assigned explicitly in seed/data layer.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Type of the object (apartment, house, office, etc.).
    /// </summary>
    public required PropertyType Type { get; init; }

    /// <summary>
    /// Purpose of the object (residential or commercial).
    /// </summary>
    public required PropertyPurpose Purpose { get; init; }

    /// <summary>
    /// Cadastral number of the object.
    /// </summary>
    public required string CadastralNumber { get; init; }

    /// <summary>
    /// Postal address of the object.
    /// </summary>
    public required string Address { get; init; }

    /// <summary>
    /// Total storeys (building height in floors).
    /// </summary>
    public required int FloorsTotal { get; init; }

    /// <summary>
    /// Total area in square meters.
    /// </summary>
    public required decimal TotalAreaSqM { get; init; }

    /// <summary>
    /// Number of rooms (applicable for residential units).
    /// </summary>
    public required int Rooms { get; init; }

    /// <summary>
    /// Ceiling height in meters.
    /// </summary>
    public required decimal CeilingHeightM { get; init; }

    /// <summary>
    /// Floor number where the unit is located (null for land/house).
    /// </summary>
    public int? Floor { get; init; }

    /// <summary>
    /// Whether the object has encumbrances (mortgage, lease, etc.).
    /// </summary>
    public required bool HasEncumbrances { get; init; }
}
