using Housemate.Application.Models.HousingTasks;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Services.Abstractions;

namespace Housemate.Application.Services;

public sealed class HousingTaskService : IHousingTaskService
{
    private readonly IHousingTaskRepository _housingTaskRepository;

    public HousingTaskService(IHousingTaskRepository housingTaskRepository)
    {
        _housingTaskRepository = housingTaskRepository;
    }

    public Task<IEnumerable<HousingTask>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _housingTaskRepository.GetAllAsync(cancellationToken);
    }

    public Task<HousingTask?> GetByIdAsync(string housingTaskId, CancellationToken cancellationToken = default)
    {
        return _housingTaskRepository.GetByIdAsync(housingTaskId, cancellationToken);
    }

    public Task<bool> CreateAsync(HousingTask housingTask, CancellationToken cancellationToken = default)
    {
        return _housingTaskRepository.CreateAsync(housingTask, cancellationToken);
    }

    public Task<HousingTask?> UpdateAsync(string housingTaskId, HousingTask housingTask, CancellationToken cancellationToken = default)
    {
        return _housingTaskRepository.UpdateAsync(housingTask, cancellationToken);
    }

    public Task<bool> DeleteByIdAsync(string housingTaskId, CancellationToken cancellationToken = default)
    {
        return _housingTaskRepository.DeleteByIdAsync(housingTaskId, cancellationToken);
    }
}