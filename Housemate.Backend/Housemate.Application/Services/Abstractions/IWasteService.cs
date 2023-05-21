using Housemate.Application.Models.Wastes;

namespace Housemate.Application.Services.Abstractions;

public interface IWasteService
{
    Task<IEnumerable<Waste>> GetAllAsync(CancellationToken cancellationToken = default);
    
    Task<Waste?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(Waste waste, CancellationToken cancellationToken = default);

    Task<Waste?> UpdateAsync(Waste waste, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
}