using AutoMapper;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Domain;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Services;

/// <summary>
/// Service that provides CRUD operations for <see cref="Counterparty"/> using DTOs and a repository.
/// </summary>
/// <param name="repo">Repository for accessing and modifying counterparties.</param>
/// <param name="requestRepo">Repository for accessing and modifying requests.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class CounterpartyService(IRepository<Counterparty, int> repo, IRepository<RealEstateRequest, int> requestRepo, IMapper mapper)
    : ICounterpartyService
{
    /// <summary>
    /// Creates a counterparty from the provided DTO.
    /// </summary>
    /// <param name="dto">Counterparty data to create.</param>
    /// <returns>Created counterparty DTO.</returns>
    public async Task<CounterpartyDto> Create(CounterpartyCreateUpdateDto dto)
    {
        var entity = mapper.Map<Counterparty>(dto);
        var created = await repo.Create(entity);
        return mapper.Map<CounterpartyDto>(created);
    }

    /// <summary>
    /// Gets a counterparty by id.
    /// </summary>
    /// <param name="dtoId">Counterparty id.</param>
    /// <returns>Counterparty DTO if found; otherwise null.</returns>
    public async Task<CounterpartyDto?> Get(int dtoId)
    {
        var entity = await repo.Get(dtoId);
        return mapper.Map<CounterpartyDto>(entity);
    }

    /// <summary>
    /// Gets all counterparties.
    /// </summary>
    /// <returns>List of counterparty DTOs.</returns>
    public async Task<IList<CounterpartyDto>> GetAll()
    {
        var entities = await repo.GetAll();
        return [.. entities.Select(mapper.Map<CounterpartyDto>)];
    }

    /// <summary>
    /// Updates an existing counterparty by id using the provided DTO.
    /// </summary>
    /// <param name="dto">Counterparty data to update.</param>
    /// <param name="dtoId">Counterparty id.</param>
    /// <returns>Updated counterparty DTO.</returns>
    public async Task<CounterpartyDto> Update(CounterpartyCreateUpdateDto dto, int dtoId)
    {
        var existing = await repo.Get(dtoId) ?? throw new KeyNotFoundException($"Counterparty with id={dtoId} was not found.");

        mapper.Map(dto, existing);

        var saved = await repo.Update(existing);
        return mapper.Map<CounterpartyDto>(saved);
    }

    /// <summary>
    /// Deletes a counterparty by id.
    /// </summary>
    /// <param name="dtoId">Counterparty id.</param>
    /// <returns>True if deleted; otherwise false.</returns>
    public async Task<bool> Delete(int dtoId) => await repo.Delete(dtoId);

    /// <summary>
    /// Gets all requests created by the specified counterparty.
    /// </summary>
    /// <param name="id">Counterparty identifier.</param>
    /// <returns>List of request DTOs created by the given counterparty.</returns>
    public async Task<IList<RealEstateRequestDto>> GetRealEstateRequests(int id)
    {
        var counterparty = await repo.Get(id) ?? throw new KeyNotFoundException($"Counterparty with id={id} was not found.");

        var requests = await requestRepo.GetAll();

        var counterpartyRequests = requests
            .Where(r  => r.ClientId == id)
            .ToList();

        return mapper.Map<IList<RealEstateRequestDto>>(counterpartyRequests);
    }
}