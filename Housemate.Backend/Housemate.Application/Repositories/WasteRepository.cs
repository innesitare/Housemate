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

    public async Task<Waste?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(Waste housingTask, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Waste?> UpdateAsync(Waste housingTask, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}