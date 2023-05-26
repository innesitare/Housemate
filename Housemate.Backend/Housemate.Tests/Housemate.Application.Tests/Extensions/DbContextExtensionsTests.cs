using FluentAssertions;
using Housemate.Application.Context;
using Housemate.Application.Extensions;
using Housemate.Application.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using Xunit;

namespace Housemate.Application.Tests.Extensions;

public sealed class DatabaseExtensionsTests : IClassFixture<MySqlContainerFixture>
{
    private readonly MySqlContainer _mySqlContainer;
    
    public DatabaseExtensionsTests(MySqlContainerFixture fixture)
    {
        _mySqlContainer = fixture.MySqlContainer;
    }
    
    [Fact]
    public async Task AddDatabase_Should_RegisterDbContextInDIContainerAsync()
    {
        // Arrange
        var services = new ServiceCollection();
        var connectionString = $"Server={_mySqlContainer.Hostname};Port={_mySqlContainer.GetMappedPublicPort(3306)};Database=testStore;User ID=admin;Password=admin;SSL Mode=Required;";

        // Act
        services.AddDatabase<ApplicationDbContext>(connectionString);
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetService<ApplicationDbContext>();
        
        // Assert
        dbContext.Should().NotBeNull();
        dbContext!.Database.Should().NotBeNull();
        dbContext.Database.GetConnectionString().Should().NotBeNull();
        
        await Task.CompletedTask;
    }
}