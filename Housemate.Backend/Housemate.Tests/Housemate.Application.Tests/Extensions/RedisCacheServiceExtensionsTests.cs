using FluentAssertions;
using Housemate.Application.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Housemate.Application.Tests.Extensions;

public sealed class RedisCacheServiceExtensionsTests
{
    [Fact]
    public void AddRedisCache_Should_RegisterRedisCacheService()
    {
        // Arrange
        var services = new ServiceCollection();
        const string connectionString = "localhost:6379";

        // Act
        services.AddRedisCache(connectionString);
        var serviceProvider = services.BuildServiceProvider();

        // Assert
        serviceProvider.Should().NotBeNull();
        serviceProvider.GetService<IDistributedCache>().Should().NotBeNull();
    }
}