using Housemate.Application.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Housemate.Application.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        PasswordOptions? passwordOptions = null, UserOptions? userOptions = null)
    {
        passwordOptions ??= new PasswordOptions
        {
            RequireDigit = true,
            RequiredLength = 6,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = true
        };

        userOptions ??= new UserOptions
        {
            RequireUniqueEmail = true
        };

        services.AddIdentity<ApplicationUser, IdentityRole>(x =>
            {
                x.Password = passwordOptions;
                x.User = userOptions;
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
}