using FluentAssertions;
using Housemate.Application.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Housemate.Application.Tests.Extensions;

public sealed class FilterExtensionsTests
{
    private sealed class ExampleFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return next();
        }
    }

    [Fact]
    public void AddFilter_ShouldAddSingletonService()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddFilter<ExampleFilter>();

        // Assert
        services.Should().Contain(x => x.ServiceType == typeof(ExampleFilter)
                                       && x.ImplementationType == typeof(ExampleFilter)
                                       && x.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void MapFiltersFrom_ShouldScanAssemblyAndRegisterFilters()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.MapFiltersFrom<ExampleFilter>();

        // Assert
        services.Should().Contain(x => x.ServiceType == typeof(IAsyncActionFilter)
                                       && x.ImplementationType == typeof(ExampleFilter)
                                       && x.Lifetime == ServiceLifetime.Scoped);
    }
}