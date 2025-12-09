using Microsoft.EntityFrameworkCore;
using RealEstate.Domain;
using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="RealEstateObject"/>.
/// </summary>
public class RealEstateObjectRepository(RealEstateDbContext db) : IRepository<RealEstateObject, int>
{
    /// <inheritdoc />
    public async Task<RealEstateObject> Create(RealEstateObject entity)
    {
        db.RealEstateObjects.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public Task<RealEstateObject?> Get(int entityId) =>
        db.RealEstateObjects.FirstOrDefaultAsync(x => x.Id == entityId);

    /// <inheritdoc />
    public async Task<IList<RealEstateObject>> GetAll() =>
        await db.RealEstateObjects.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<RealEstateObject> Update(RealEstateObject entity)
    {
        db.RealEstateObjects.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int entityId)
    {
        var existing = await db.RealEstateObjects.FirstOrDefaultAsync(x => x.Id == entityId);
        if (existing is null) return false;

        db.RealEstateObjects.Remove(existing);
        await db.SaveChangesAsync();
        return true;
    }
}