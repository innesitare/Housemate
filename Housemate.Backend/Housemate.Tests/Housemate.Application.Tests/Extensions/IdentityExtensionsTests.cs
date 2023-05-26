using FluentAssertions;
using Housemate.Application.Context;
using Housemate.Application.Extensions;
using Housemate.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Housemate.Application.Tests.Extensions;

public sealed class IdentityExtensionsTests
{
    [Fact]
    public void AddIdentityConfiguration_ShouldConfigurePasswordOptions_WhenCalledWithPasswordOptions()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddIdentityConfiguration(new PasswordOptions
        {
            RequireDigit = true,
            RequiredLength = 10,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = true
        });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<IdentityOptions>>()!.Value.Password;

        options.RequireDigit.Should().BeTrue();
        options.RequiredLength.Should().Be(10);
        options.RequireLowercase.Should().BeTrue();
        options.RequireUppercase.Should().BeTrue();
        options.RequireNonAlphanumeric.Should().BeTrue();
    }

    [Fact]
    public void AddIdentityConfiguration_ShouldConfigureUserOptions_WhenCalledWithUserOptions()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddIdentityConfiguration(userOptions: new UserOptions
        {
            RequireUniqueEmail = false
        });

        // Assert
        var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider.GetService<IOptions<IdentityOptions>>()!.Value.User;

        options.RequireUniqueEmail.Should().BeFalse();
    }

    [Fact]
    public void AddIdentityConfiguration_ShouldRegisterIdentityTypes()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        // Act
        services.AddIdentityConfiguration();

        // Assert
        services.Should().Contain(x => x.ServiceType == typeof(UserManager<ApplicationUser>));
        services.Should().Contain(x => x.ServiceType == typeof(SignInManager<ApplicationUser>));
        services.Should().Contain(x => x.ServiceType == typeof(RoleManager<IdentityRole>));
    }

    [Fact]
    public void AddIdentityConfiguration_ShouldUseEntityFrameworkStores()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

        // Act
        services.AddIdentityConfiguration();

        // Assert
        services.Should().Contain(x => x.ServiceType == typeof(IUserStore<ApplicationUser>));
        services.Should().Contain(x => x.ServiceType == typeof(IRoleStore<IdentityRole>));
    }
}