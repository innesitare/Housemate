using Housemate.Application.Attributes;
using Housemate.Application.Helpers;
using Housemate.Application.Models.Wastes;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Services.Abstractions;

namespace Housemate.Application.Repositories.CachedRepositories;

[CachingDecorator]
public sealed class CachedWasteRepository : IWasteRepository
{
    private readonly IWasteRepository _wasteRepository;
    private readonly ICacheService<Waste> _cacheService;

    public CachedWasteRepository(IWasteRepository wasteRepository, ICacheService<Waste> cacheService)
    {
        _wasteRepository = wasteRepository;
        _cacheService = cacheService;
    }

    public Task<IEnumerable<Waste>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.Waste.GetAll, async () =>
        {
            var wastes = await _wasteRepository.GetAllAsync(cancellationToken);
            return wastes;
        }, cancellationToken);
    }

    public Task<Waste?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Waste.Get(id), async () =>
        {
            var waste = await _wasteRepository.GetByIdAsync(id, cancellationToken);
            return waste;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(Waste waste, CancellationToken cancellationToken = default)
    {
        bool created = await _wasteRepository.CreateAsync(waste, cancellationToken);
        if (created)
        {
            await _cacheService.RemoveAsync(CacheKeys.Waste.GetAll, cancellationToken);
        }

        return created;
    }

    public async Task<Waste?> UpdateAsync(Waste waste, CancellationToken cancellationToken = default)
    {
        var updated = await _wasteRepository.UpdateAsync(waste, cancellationToken);
        if (updated is null)    
        {
            return updated;
        }

        await _cacheService.RemoveAsync(CacheKeys.Waste.Get(waste.Id), cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.Waste.GetAll, cancellationToken);

        return updated;
    }

    public async Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        bool deleted = await _wasteRepository.DeleteByIdAsync(id, cancellationToken);
        if (!deleted)
        {
            return deleted;
        }
        
        await _cacheService.RemoveAsync(CacheKeys.Waste.GetAll, cancellationToken);
        await _cacheService.RemoveAsync(CacheKeys.Waste.Get(id), cancellationToken);
        
        return deleted;
    }
}