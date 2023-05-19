using Housemate.Application.Models.HousingTasks;

namespace Housemate.Application.Services.Abstractions;

public interface IHousingTaskService
{
    Task<IEnumerable<HousingTask>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<HousingTask?> GetByIdAsync(string housingTaskId, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(HousingTask housingTask, CancellationToken cancellationToken = default);

    Task<HousingTask?> UpdateAsync(string housingTaskId, HousingTask housingTask, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(string housingTaskId, CancellationToken cancellationToken = default);
}