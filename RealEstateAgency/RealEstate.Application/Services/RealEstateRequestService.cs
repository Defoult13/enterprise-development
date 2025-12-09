using AutoMapper;
using RealEstate.Application.Contracts.Counterparties;
using RealEstate.Application.Contracts.RealEstateObjects;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Domain;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Services;

/// <summary>
/// Service that provides CRUD operations for <see cref="RealEstateRequest"/> using DTOs and repositories,
/// and also allows retrieving referenced counterparties and real-estate objects.
/// </summary>
/// <param name="requestRepo">Repository for accessing and modifying requests.</param>
/// <param name="counterpartyRepo">Repository for accessing counterparties.</param>
/// <param name="realEstateObjectRepo">Repository for accessing real-estate objects.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class RealEstateRequestService(
    IRepository<RealEstateRequest, int> requestRepo,
    IRepository<Counterparty, int> counterpartyRepo,
    IRepository<RealEstateObject, int> realEstateObjectRepo,
    IMapper mapper)
    : IRealEstateRequestService
{
    /// <summary>
    /// Creates a request from the provided DTO.
    /// </summary>
    /// <param name="dto">Request data to create.</param>
    /// <returns>Created request DTO.</returns>
    public async Task<RealEstateRequestDto> Create(RealEstateRequestCreateUpdateDto dto)
    {
        _ = await counterpartyRepo.Get(dto.ClientId)
            ?? throw new KeyNotFoundException($"Counterparty with id={dto.ClientId} was not found.");

        _ = await realEstateObjectRepo.Get(dto.PropertyId)
            ?? throw new KeyNotFoundException($"RealEstateObject with id={dto.PropertyId} was not found.");

        var entity = mapper.Map<RealEstateRequest>(dto);
        var created = await requestRepo.Create(entity);
        return mapper.Map<RealEstateRequestDto>(created);
    }

    /// <summary>
    /// Gets a request by id.
    /// </summary>
    /// <param name="dtoId">Request id.</param>
    /// <returns>Request DTO if found; otherwise null.</returns>
    public async Task<RealEstateRequestDto?> Get(int dtoId)
    {
        var entity = await requestRepo.Get(dtoId);
        return mapper.Map<RealEstateRequestDto>(entity);
    }

    /// <summary>
    /// Gets all requests.
    /// </summary>
    /// <returns>List of request DTOs.</returns>
    public async Task<IList<RealEstateRequestDto>> GetAll()
    {
        var entities = await requestRepo.GetAll();
        return [.. entities.Select(mapper.Map<RealEstateRequestDto>)];
    }

    /// <summary>
    /// Updates an existing request by id using the provided DTO.
    /// </summary>
    /// <param name="dto">Request data to update.</param>
    /// <param name="dtoId">Request id.</param>
    /// <returns>Updated request DTO.</returns>
    public async Task<RealEstateRequestDto> Update(RealEstateRequestCreateUpdateDto dto, int dtoId)
    {
        var existing = await requestRepo.Get(dtoId) ?? throw new KeyNotFoundException($"RealEstateRequest with id={dtoId} was not found.");

        _ = await counterpartyRepo.Get(dto.ClientId)
            ?? throw new KeyNotFoundException($"Counterparty with id={dto.ClientId} was not found.");

        _ = await realEstateObjectRepo.Get(dto.PropertyId)
            ?? throw new KeyNotFoundException($"RealEstateObject with id={dto.PropertyId} was not found.");

        mapper.Map(dto, existing);

        var saved = await requestRepo.Update(existing);
        return mapper.Map<RealEstateRequestDto>(saved);
    }

    /// <summary>
    /// Deletes a request by id.
    /// </summary>
    /// <param name="dtoId">Request id.</param>
    /// <returns>True if deleted; otherwise false.</returns>
    public async Task<bool> Delete(int dtoId) => await requestRepo.Delete(dtoId);

    /// <summary>
    /// Gets a counterparty by id.
    /// </summary>
    /// <param name="id">Counterparty id.</param>
    /// <returns>Counterparty DTO.</returns>
    public async Task<CounterpartyDto> GetCounterparty(int id)
    {
        var entity = await counterpartyRepo.Get(id) ?? throw new KeyNotFoundException($"Counterparty with id={id} was not found.");
        return mapper.Map<CounterpartyDto>(entity);
    }

    /// <summary>
    /// Gets a real-estate object by id.
    /// </summary>
    /// <param name="id">Real-estate object id.</param>
    /// <returns>Real-estate object DTO.</returns>
    public async Task<RealEstateObjectDto> GetRealEstate(int id)
    {
        var entity = await realEstateObjectRepo.Get(id) ?? throw new KeyNotFoundException($"RealEstateObject with id={id} was not found.");
        return mapper.Map<RealEstateObjectDto>(entity);
    }
}