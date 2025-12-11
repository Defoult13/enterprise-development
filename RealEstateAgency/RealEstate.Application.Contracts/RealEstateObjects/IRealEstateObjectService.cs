using RealEstate.Application.Contracts.RealEstateRequests;

namespace RealEstate.Application.Contracts.RealEstateObjects;

/// <summary>
/// Application service contract for real-estate objects with basic CRUD operations
/// and access to related requests.
/// </summary>
public interface IRealEstateObjectService : IApplicationService<RealEstateObjectDto, RealEstateObjectCreateUpdateDto, int>
{
    /// <summary>
    /// Gets all requests related to the specified real-estate object.
    /// </summary>
    /// <param name="id">Real-estate object identifier.</param>
    /// <returns>List of request DTOs for the given real-estate object.</returns>
    public Task<IList<RealEstateRequestDto>> GetRealEstateRequests(int id);
}