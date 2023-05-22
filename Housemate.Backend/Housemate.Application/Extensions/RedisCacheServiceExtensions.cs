using Microsoft.Extensions.DependencyInjection;

namespace Housemate.Application.Extensions;

public static class RedisCacheServiceExtensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, string connectionString)
    {
        services.AddStackExchangeRedisCache(x => x.Configuration = connectionString);

        return services;
    }
}