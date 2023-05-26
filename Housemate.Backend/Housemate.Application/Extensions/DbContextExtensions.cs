using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Housemate.Application.Extensions;

public static class DbContextExtensions
{
    public static IServiceCollection AddDatabase<TEntity>(this IServiceCollection services, string connectionString)
        where TEntity : DbContext
    {
        services.AddMySql<TEntity>(connectionString, ServerVersion.AutoDetect(connectionString));

        return services;
    }
}