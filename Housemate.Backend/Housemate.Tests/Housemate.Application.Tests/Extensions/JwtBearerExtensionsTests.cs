using FluentAssertions;
using Housemate.Application.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace Housemate.Application.Tests.Extensions;

public sealed class JwtBearerExtensionsTests
{
    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>("Jwt:Audience", "test-audience"),
                new KeyValuePair<string, string>("Jwt:Issuer", "test-issuer"),
                new KeyValuePair<string, string>("Jwt:Key", "test-key")
            }!)
            .AddUserSecrets<JwtBearerExtensionsTests>()
            .Build();
    }

    [Fact]
    public Task AddJwtBearer_ShouldConfigureJwtBearerAuthentication()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        
        var configuration = GetConfiguration();
        builder.Configuration.AddConfiguration(configuration);

        // Act
        var result = builder.AddJwtBearer();

        // Assert
        result.Should().BeEquivalentTo(builder);
        var app = result.Build();

        // Authentication
        app.UseAuthentication();
        app.UseAuthorization();

        // Services
        var serviceProvider = app.Services;

        serviceProvider.GetService<IAuthorizationService>().Should().NotBeNull();
        serviceProvider.GetService<IAuthenticationService>().Should().NotBeNull();

        // AuthorizationOptions
        var authorizationOptions = serviceProvider.GetRequiredService<IOptions<AuthorizationOptions>>().Value;
        
        authorizationOptions.Should().NotBeNull();
        authorizationOptions.DefaultPolicy.Should().NotBeNull();
        
        authorizationOptions.GetPolicy("Bearer")!.Should().NotBeNull();
        authorizationOptions.GetPolicy("Bearer")!.AuthenticationSchemes.Should().Contain(JwtBearerDefaults.AuthenticationScheme);

        // AuthenticationOptions
        var authenticationOptions = serviceProvider.GetRequiredService<IOptions<AuthenticationOptions>>().Value;
        
        authenticationOptions.Should().NotBeNull();
        
        authenticationOptions.DefaultScheme.Should().Be(JwtBearerDefaults.AuthenticationScheme);
        authenticationOptions.DefaultAuthenticateScheme.Should().Be(JwtBearerDefaults.AuthenticationScheme);
        authenticationOptions.DefaultChallengeScheme.Should().Be(JwtBearerDefaults.AuthenticationScheme);

        // JwtBearerOptions
        var jwtBearerOptions = serviceProvider.GetRequiredService<IOptionsMonitor<JwtBearerOptions>>().Get("Bearer");
        
        jwtBearerOptions.TokenValidationParameters.Should().NotBeNull();
        
        jwtBearerOptions.TokenValidationParameters.ValidateIssuer.Should().BeTrue();
        jwtBearerOptions.TokenValidationParameters.ValidateAudience.Should().BeTrue();
        jwtBearerOptions.TokenValidationParameters.ValidateIssuerSigningKey.Should().BeTrue();
        jwtBearerOptions.TokenValidationParameters.ValidIssuer.Should().Be("test-issuer");
        jwtBearerOptions.TokenValidationParameters.ValidAudience.Should().Be("test-audience");
        
        jwtBearerOptions.TokenValidationParameters.IssuerSigningKey.Should().NotBeNull();
        jwtBearerOptions.TokenValidationParameters.IssuerSigningKey.Should().BeOfType<SymmetricSecurityKey>();

        // AuthenticationSchemeProvider
        var authenticationSchemeProvider = serviceProvider.GetService<IAuthenticationSchemeProvider>();
        var authenticationSchemes = authenticationSchemeProvider?.GetAllSchemesAsync().Result;
        
        authenticationSchemes.Should().Contain(scheme => scheme.Name == JwtBearerDefaults.AuthenticationScheme);

        // Additional services
        serviceProvider.GetService<IOptions<JwtBearerOptions>>().Should().NotBeNull();
        serviceProvider.GetService<IOptionsMonitorCache<JwtBearerOptions>>().Should().NotBeNull();
        serviceProvider.GetService<IAuthenticationHandlerProvider>().Should().NotBeNull();
        serviceProvider.GetService<ISystemClock>().Should().NotBeNull();
        
        return Task.CompletedTask;
    }
}