namespace RealEstate.Application.Contracts;

/// <summary>
/// Application service contract with basic CRUD operations for DTOs.
/// </summary>
/// <typeparam name="TDto">DTO type returned by the service.</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO type used for creating and updating.</typeparam>
/// <typeparam name="TKey">Key type (e.g., int).</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity from the provided DTO and returns the created DTO.
    /// </summary>
    /// <param name="dto">Data for creating the entity.</param>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Gets a DTO by id. Returns null if not found.
    /// </summary>
    /// <param name="dtoId">DTO identifier.</param>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Gets all DTOs.
    /// </summary>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Updates an entity by id using the provided DTO and returns the updated DTO.
    /// </summary>
    /// <param name="dto">Data for updating the entity.</param>
    /// <param name="dtoId">DTO identifier.</param>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Deletes an entity by id. Returns true if deleted; otherwise false.
    /// </summary>
    /// <param name="dtoId">DTO identifier.</param>
    public Task<bool> Delete(TKey dtoId);
}