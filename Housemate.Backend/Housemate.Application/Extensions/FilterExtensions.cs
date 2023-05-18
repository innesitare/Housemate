using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Housemate.Application.Extensions;

public static class FilterExtensions
{
    public static IServiceCollection AddFilter<TEntity>(this IServiceCollection services)
        where TEntity : class, IAsyncActionFilter
    {
        services.AddSingleton<TEntity>();

        return services;
    }
    
    public static IServiceCollection MapFiltersFrom<TEntity>(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<TEntity>()
            .AddClasses(classes => classes.AssignableTo<IAsyncActionFilter>())
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        return services;
    }
}