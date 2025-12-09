using Microsoft.EntityFrameworkCore;
using RealEstate.Domain;
using RealEstate.Domain.Models;

namespace RealEstate.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core repository for <see cref="Counterparty"/>.
/// </summary>
public class CounterpartyRepository(RealEstateDbContext db) : IRepository<Counterparty, int>
{
    /// <inheritdoc />
    public async Task<Counterparty> Create(Counterparty entity)
    {
        db.Counterparties.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public Task<Counterparty?> Get(int entityId) =>
        db.Counterparties.FirstOrDefaultAsync(x => x.Id == entityId);

    /// <inheritdoc />
    public async Task<IList<Counterparty>> GetAll() =>
        await db.Counterparties.AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<Counterparty> Update(Counterparty entity)
    {
        db.Counterparties.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int entityId)
    {
        var existing = await db.Counterparties.FirstOrDefaultAsync(x => x.Id == entityId);
        if (existing is null) return false;

        db.Counterparties.Remove(existing);
        await db.SaveChangesAsync();
        return true;
    }
}