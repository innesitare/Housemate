using Housemate.Application.Models.HousingTasks;

namespace Housemate.Application.Services.Abstractions;

public interface IHousingTaskService
{
    Task<IEnumerable<HousingTask>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<HousingTask?> GetByIdAsync(string housingTaskId, string orderId, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(string housingTaskId, HousingTask housingTask, CancellationToken cancellationToken = default);

    Task<HousingTask?> UpdateAsync(string housingTaskId, string orderId, HousingTask housingTask,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(string housingTaskId, string orderId, CancellationToken cancellationToken = default);
}