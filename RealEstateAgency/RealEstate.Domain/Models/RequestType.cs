namespace RealEstate.Domain.Models;

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
