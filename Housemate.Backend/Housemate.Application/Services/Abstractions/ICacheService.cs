namespace Housemate.Application.Services.Abstractions;

public interface ICacheService<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(string key, CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(string key, CancellationToken cancellationToken = default);

    Task SetAsync(string key, string jsonEntity, CancellationToken cancellationToken = default);

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    Task<TEntity?> GetOrCreateAsync(string key, Func<Task<TEntity?>> createEntity,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllOrCreateAsync(string key, Func<Task<IEnumerable<TEntity>>> createEntity,
        CancellationToken cancellationToken = default);
}