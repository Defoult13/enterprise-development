namespace RealEstate.Domain.Models;

/// <summary>
/// Specific type of a real-estate object.
/// </summary>
public enum PropertyType
{
    /// <summary>
    /// An apartment located in a multi-storey building.
    /// </summary>
    Apartment,

    /// <summary>
    /// A standalone residential house.
    /// </summary>
    House,

    /// <summary>
    /// A townhouse (row house / terraced house).
    /// </summary>
    Townhouse,

    /// <summary>
    /// An office space.
    /// </summary>
    Office,

    /// <summary>
    /// A retail unit (shop, pavilion, etc.).
    /// </summary>
    Retail,

    /// <summary>
    /// A warehouse facility.
    /// </summary>
    Warehouse,

    /// <summary>
    /// A land plot.
    /// </summary>
    Land
}
