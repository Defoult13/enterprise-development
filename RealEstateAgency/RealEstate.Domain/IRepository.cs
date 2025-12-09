namespace RealEstate.Domain;

/// <summary>
/// Repository with basic CRUD operations
/// for an entity <typeparamref name="TEntity"/> with key <typeparamref name="TKey"/>.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TKey">Key type.</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">Entity to create.</param>
    /// <returns>The created entity.</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Retrieves an entity by its identifier.
    /// </summary>
    /// <param name="entityId">Entity identifier.</param>
    /// <returns>The entity if found; otherwise null.</returns>
    public Task<TEntity?> Get(TKey entityId);

    /// <summary>
    /// Retrieves all entities of the given type.
    /// </summary>
    /// <returns>List of all entities.</returns>
    public Task<IList<TEntity>> GetAll();

    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    /// <param name="entity">Entity with updated data.</param>
    /// <returns>The updated entity.</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Deletes an entity by id.
    /// </summary>
    /// <param name="entityId">Entity identifier.</param>
    /// <returns>true if the entity existed and was deleted; otherwise false.</returns>
    public Task<bool> Delete(TKey entityId);
}