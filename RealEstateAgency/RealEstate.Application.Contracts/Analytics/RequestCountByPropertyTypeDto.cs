using RealEstate.Domain.Shared.Enums;

namespace RealEstate.Application.Contracts.Analytics;

/// <summary>
/// Request count for a property type.
/// </summary>
/// <param name="PropertyType">Property type.</param>
/// <param name="Count">Requests count.</param>
public sealed record RequestCountByPropertyTypeDto(
    PropertyType PropertyType,
    int Count
);