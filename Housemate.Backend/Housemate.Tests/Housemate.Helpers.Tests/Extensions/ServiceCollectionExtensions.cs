using Housemate.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.MySql;
using Testcontainers.Redis;

namespace Housemate.Helpers.Tests.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureTestRedisContainer(this IServiceCollection services,
        RedisContainer redisContainer)
    {
        services.RemoveAll<RedisCacheOptions>();
        services.AddRedisCache(redisContainer.GetConnectionString());

        return services;
    }

    public static IServiceCollection ConfigureTestDbContext<TDbContext>(this IServiceCollection services,
        MySqlContainer mySqlContainer)
        where TDbContext : DbContext
    {
        services.RemoveAll<DbContextOptions<TDbContext>>();
        services.AddDatabase<TDbContext>(mySqlContainer.GetConnectionString());

        return services;
    }
}