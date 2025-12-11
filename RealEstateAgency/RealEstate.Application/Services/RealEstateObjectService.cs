using AutoMapper;
using RealEstate.Application.Contracts.RealEstateObjects;
using RealEstate.Application.Contracts.RealEstateRequests;
using RealEstate.Domain;
using RealEstate.Domain.Models;

namespace RealEstate.Application.Services;

/// <summary>
/// Service that provides CRUD operations for <see cref="RealEstateObject"/> using DTOs and a repository.
/// </summary>
/// <param name="repo">Repository for accessing and modifying real-estate objects.</param>
/// <param name="requestRepo">Repository for accessing and modifying requests.</param>
/// <param name="mapper">Mapper used to convert between entities and DTOs.</param>
public class RealEstateObjectService(IRepository<RealEstateObject, int> repo, IRepository<RealEstateRequest, int> requestRepo, IMapper mapper)
    : IRealEstateObjectService
{
    /// <summary>
    /// Creates a real-estate object from the provided DTO.
    /// </summary>
    /// <param name="dto">Real-estate object data to create.</param>
    /// <returns>Created real-estate object DTO.</returns>
    public async Task<RealEstateObjectDto> Create(RealEstateObjectCreateUpdateDto dto)
    {
        var entity = mapper.Map<RealEstateObject>(dto);
        var created = await repo.Create(entity);
        return mapper.Map<RealEstateObjectDto>(created);
    }

    /// <summary>
    /// Gets a real-estate object by id.
    /// </summary>
    /// <param name="dtoId">Real-estate object id.</param>
    /// <returns>Real-estate object DTO if found; otherwise null.</returns>
    public async Task<RealEstateObjectDto?> Get(int dtoId)
    {
        var entity = await repo.Get(dtoId);
        return mapper.Map<RealEstateObjectDto>(entity);
    }

    /// <summary>
    /// Gets all real-estate objects.
    /// </summary>
    /// <returns>List of real-estate object DTOs.</returns>
    public async Task<IList<RealEstateObjectDto>> GetAll()
    {
        var entities = await repo.GetAll();
        return [.. entities.Select(mapper.Map<RealEstateObjectDto>)];
    }

    /// <summary>
    /// Updates an existing real-estate object by id using the provided DTO.
    /// </summary>
    /// <param name="dto">Real-estate object data to update.</param>
    /// <param name="dtoId">Real-estate object id.</param>
    /// <returns>Updated real-estate object DTO.</returns>
    public async Task<RealEstateObjectDto> Update(RealEstateObjectCreateUpdateDto dto, int dtoId)
    {
        var existing = await repo.Get(dtoId) ?? throw new KeyNotFoundException($"RealEstateObject with id={dtoId} was not found.");

        mapper.Map(dto, existing);

        var saved = await repo.Update(existing);
        return mapper.Map<RealEstateObjectDto>(saved);
    }

    /// <summary>
    /// Deletes a real-estate object by id.
    /// </summary>
    /// <param name="dtoId">Real-estate object id.</param>
    /// <returns>True if deleted; otherwise false.</returns>
    public async Task<bool> Delete(int dtoId) => await repo.Delete(dtoId);

    /// <summary>
    /// Gets all requests related to the specified real-estate object.
    /// </summary>
    /// <param name="id">Real-estate object identifier.</param>
    /// <returns>List of request DTOs for the given real-estate object.</returns>
    public async Task<IList<RealEstateRequestDto>> GetRealEstateRequests(int id)
    {
        var realEstateObject = await repo.Get(id) ?? throw new KeyNotFoundException($"RealEstateObject with id={id} was not found.");

        var requests = await requestRepo.GetAll();

        var objectRequests = requests
            .Where(r => r.PropertyId == id)
            .ToList();

        return mapper.Map<IList<RealEstateRequestDto>>(objectRequests);
    }
}