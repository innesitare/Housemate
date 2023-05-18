namespace Housemate.Application.Repositories.Abstractions;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<bool> CreateAsync(TEntity housingTask, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TEntity housingTask, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
}