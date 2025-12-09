namespace RealEstate.Domain.Models;

/// <summary>
/// A client request to buy or sell a specific real-estate object.
/// </summary>
public sealed class RealEstateRequest
{
    /// <summary>
    /// Integer identifier assigned explicitly in seed/data layer.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Foreign key to Counterparty.
    /// Required.
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Foreign key to RealEstateObject.
    /// Required.
    /// </summary>
    public required int PropertyId { get; set; }

    /// <summary>
    /// Client who placed the request.
    /// </summary>
    public Counterparty? Client { get; set; }

    /// <summary>
    /// Real-estate object the request refers to.
    /// </summary>
    public RealEstateObject? Property { get; set; }

    /// <summary>
    /// Request type: buy or sell.
    /// </summary>
    public required RequestType Type { get; set; }

    /// <summary>
    /// Monetary amount stated in the request.
    /// </summary>
    public required decimal Amount { get; set; }

    /// <summary>
    /// Request creation date (no time component).
    /// </summary>
    public required DateOnly CreatedAt { get; init; }
}
