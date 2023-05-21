using Housemate.Application.Context;
using Housemate.Application.Models.Wastes;
using Housemate.Application.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Housemate.Application.Repositories;

public sealed class WasteRepository : IWasteRepository
{
    private readonly ApplicationDbContext _dbContext;

    public WasteRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Waste>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        bool isEmpty = await _dbContext.Wastes.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<Waste>();
        }

        return _dbContext.Wastes;
    }

    public async Task<Waste?> GetByIdAsync(string wasteId, CancellationToken cancellationToken = default)
    {
        var waste = await _dbContext.Wastes.FindAsync(new object?[] {wasteId}, cancellationToken);

        return waste;
    }

    public async Task<bool> CreateAsync(Waste waste, CancellationToken cancellationToken = default)
    {
        await _dbContext.Wastes.AddAsync(waste, cancellationToken);
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<Waste?> UpdateAsync(Waste waste, CancellationToken cancellationToken = default)
    {
        bool isContains = await _dbContext.Wastes.ContainsAsync(waste, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _dbContext.Wastes.Update(waste);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return waste;
    }

    public async Task<bool> DeleteByIdAsync(string wasteId, CancellationToken cancellationToken = default)
    {
        var waste = await _dbContext.Wastes.FindAsync(new object?[] {wasteId}, cancellationToken);
        if (waste is null)
        {
            return false;
        }

        _dbContext.Wastes.Remove(waste);
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}