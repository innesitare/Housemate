using Housemate.Application.Models.Wastes;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Services.Abstractions;

namespace Housemate.Application.Services;

public sealed class WasteService : IWasteService
{
    private readonly IWasteRepository _wasteRepository;

    public WasteService(IWasteRepository wasteRepository)
    {
        _wasteRepository = wasteRepository;
    }

    public Task<IEnumerable<Waste>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _wasteRepository.GetAllAsync(cancellationToken);
    }

    public Task<Waste?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _wasteRepository.GetByIdAsync(id, cancellationToken);
    }

    public Task<bool> CreateAsync(Waste waste, CancellationToken cancellationToken = default)
    {
        return _wasteRepository.CreateAsync(waste, cancellationToken);
    }

    public Task<Waste?> UpdateAsync(Waste waste, CancellationToken cancellationToken = default)
    {
        return _wasteRepository.UpdateAsync(waste, cancellationToken);
    }

    public Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return _wasteRepository.DeleteByIdAsync(id, cancellationToken);
    }
}