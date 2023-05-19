using Housemate.Application.Context;
using Housemate.Application.Models.HousingTasks;
using Housemate.Application.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Housemate.Application.Repositories;

public sealed class HousingTaskRepository : IHousingTaskRepository
{
    private readonly ApplicationDbContext _dbContext;

    public HousingTaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<HousingTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        bool isEmpty = await _dbContext.HousingTasks.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<HousingTask>();
        }

        return _dbContext.HousingTasks;
    }

    public async Task<HousingTask?> GetByIdAsync(string housingTaskId, CancellationToken cancellationToken = default)
    {
        var housingTask = await _dbContext.HousingTasks.FindAsync(new object?[] {housingTaskId}, cancellationToken);

        return housingTask;
    }

    public async Task<bool> CreateAsync(HousingTask housingTask, CancellationToken cancellationToken = default)
    {
        await _dbContext.HousingTasks.AddAsync(housingTask, cancellationToken);
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }

    public async Task<HousingTask?> UpdateAsync(HousingTask housingTask, CancellationToken cancellationToken = default)
    {
        bool isContains = await _dbContext.HousingTasks.ContainsAsync(housingTask, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _dbContext.HousingTasks.Update(housingTask);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return housingTask;
    }

    public async Task<bool> DeleteByIdAsync(string housingTaskId, CancellationToken cancellationToken = default)
    {
        var housingTask = await _dbContext.HousingTasks.FindAsync(new object?[] {housingTaskId}, cancellationToken);
        if (housingTask is null)
        {
            return false;
        }

        _dbContext.HousingTasks.Remove(housingTask);
        int result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result > 0;
    }
}