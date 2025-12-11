using RealEstate.Application.Contracts.RealEstateRequests;

namespace RealEstate.Application.Contracts.Counterparties;

/// <summary>
/// Application service contract for counterparties with basic CRUD operations
/// and access to related requests.
/// </summary>
public interface ICounterpartyService : IApplicationService<CounterpartyDto, CounterpartyCreateUpdateDto, int>
{
    /// <summary>
    /// Gets all requests created by the specified counterparty.
    /// </summary>
    /// <param name="id">Counterparty identifier.</param>
    /// <returns>List of request DTOs created by the given counterparty.</returns>
    public Task<IList<RealEstateRequestDto>> GetRealEstateRequests(int id);
}