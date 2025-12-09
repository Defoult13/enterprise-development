using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateObjects;

namespace RealEstate.Application.Contracts.RealEstateRequests;

/// <summary>
/// Service contract for read operations related to real-estate requests.
/// Provides access to referenced entities (counterparties and real-estate objects).
/// </summary>
public interface IRealEstateRequestService : IApplicationService<RealEstateRequestDto, RealEstateRequestCreateUpdateDto, int>
{
    /// <summary>
    /// Gets a counterparty by id.
    /// </summary>
    /// <param name="id">Counterparty identifier.</param>
    /// <returns>Counterparty DTO.</returns>
    public Task<CounterpartyDto> GetCounterparty(int id);

    /// <summary>
    /// Gets a real-estate object by id.
    /// </summary>
    /// <param name="id">Real-estate object identifier.</param>
    /// <returns>Real-estate object DTO.</returns>
    public Task<RealEstateObjectDto> GetRealEstate(int id);
}