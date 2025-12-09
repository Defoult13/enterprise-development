using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Application.Contracts.RealEstateObjects;

/// <summary>
/// DTO for creating or updating a real-estate object.
/// </summary>
/// <param name="Type">Object type (apartment, house, office, etc.).</param>
/// <param name="Purpose">Object purpose (residential or commercial).</param>
/// <param name="CadastralNumber">Cadastral number.</param>
/// <param name="Address">Postal address.</param>
/// <param name="FloorsTotal">Total number of floors.</param>
/// <param name="TotalAreaSqM">Total area in square meters.</param>
/// <param name="Rooms">Rooms count.</param>
/// <param name="CeilingHeightM">Ceiling height in meters (optional).</param>
/// <param name="Floor">Floor number (optional).</param>
/// <param name="HasEncumbrances">Whether the object has encumbrances.</param>
public sealed record RealEstateObjectCreateUpdateDto(
    PropertyType Type,
    PropertyPurpose Purpose,
    string CadastralNumber,
    string Address,
    int FloorsTotal,
    double TotalAreaSqM,
    int Rooms,
    double? CeilingHeightM,
    int? Floor,
    bool HasEncumbrances
);