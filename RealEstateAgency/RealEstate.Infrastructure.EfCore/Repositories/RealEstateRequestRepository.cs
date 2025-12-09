using Microsoft.EntityFrameworkCore;
using RealEstate.Domain;
using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="RealEstateRequest"/>.
/// </summary>
public class RealEstateRequestRepository(RealEstateDbContext db) : IRepository<RealEstateRequest, int>
{
    /// <inheritdoc />
    public async Task<RealEstateRequest> Create(RealEstateRequest entity)
    {
        db.Requests.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<RealEstateRequest?> Get(int entityId) =>
        await db.Requests.FirstOrDefaultAsync(x => x.Id == entityId);

    /// <inheritdoc />
    public async Task<IList<RealEstateRequest>> GetAll() =>
        await db.Requests.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<RealEstateRequest> Update(RealEstateRequest entity)
    {
        db.Requests.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int entityId)
    {
        var existing = await db.Requests.FirstOrDefaultAsync(x => x.Id == entityId);
        if (existing is null) return false;

        db.Requests.Remove(existing);
        await db.SaveChangesAsync();
        return true;
    }
}