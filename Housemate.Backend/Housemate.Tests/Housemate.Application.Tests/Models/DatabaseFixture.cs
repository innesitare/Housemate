using Microsoft.EntityFrameworkCore;

namespace Housemate.Application.Tests.Models;

public sealed class DatabaseFixture<TEntity> : IDisposable 
    where TEntity : DbContext
{
    private readonly TEntity _dbContext;

    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<TEntity>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        _dbContext = (Activator.CreateInstance(typeof(TEntity), options) as TEntity)!;
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}