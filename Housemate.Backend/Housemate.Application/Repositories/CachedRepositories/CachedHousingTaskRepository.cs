using Housemate.Application.Attributes;
using Housemate.Application.Helpers;
using Housemate.Application.Models.HousingTasks;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Services.Abstractions;

namespace Housemate.Application.Repositories.CachedRepositories;

[CachingDecorator]
public sealed class CachedHousingTaskRepository : IHousingTaskRepository
{
    private readonly IHousingTaskRepository _housingTaskRepository;
    private readonly ICacheService<HousingTask> _cacheService;

    public CachedHousingTaskRepository(IHousingTaskRepository housingTaskRepository, ICacheService<HousingTask> cacheService)
    {
        _housingTaskRepository = housingTaskRepository;
        _cacheService = cacheService;
    }

    public Task<IEnumerable<HousingTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.HousingTask.GetAll, async () =>
        {
            var housingTasks = await _housingTaskRepository.GetAllAsync(cancellationToken);
            return housingTasks;
        }, cancellationToken);
    }

    public Task<HousingTask?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.HousingTask.Get(id), async () =>
        {
            var housingTask = await _housingTaskRepository.GetByIdAsync(id, cancellationToken);
            return housingTask;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(HousingTask housingTask, CancellationToken cancellationToken = default)
    {
        bool created = await _housingTaskRepository.CreateAsync(housingTask, cancellationToken);
        if (created)
        {
            await _cacheService.RemoveAsync(CacheKeys.HousingTask.GetAll, cancellationToken);
        }

        return created;
    }

    public async Task<HousingTask?> UpdateAsync(HousingTask housingTask, CancellationToken cancellationToken = default)
    {
        var updated = await _housingTaskRepository.UpdateAsync(housingTask, cancellationToken);
        if (updated is null)
        {
            return updated;
        }

        await _cacheService.RemoveAsync(CacheKeys.HousingTask.Get(housingTask.Id), cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.HousingTask.GetAll, cancellationToken);

        return updated;
    }

    public async Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        bool deleted = await _housingTaskRepository.DeleteByIdAsync(id, cancellationToken);
        if (!deleted)
        {
            return deleted;
        }

        await _cacheService.RemoveAsync(CacheKeys.HousingTask.GetAll, cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.HousingTask.Get(id), cancellationToken);

        return deleted;
    }
}