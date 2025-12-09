namespace RealEstate.Domain.Shared.Enums;

/// <summary>
/// Kind of client request: whether a client intends to buy or to sell.
/// </summary>
public enum RequestType
{
    /// <summary>
    /// Buying a property.
    /// </summary>
    Buy,

    /// <summary>
    /// Selling a property.
    /// </summary>
    Sell
}
