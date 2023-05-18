using Housemate.Application.Models.Identity;
using Housemate.Application.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Housemate.Application.Services;
    
public sealed class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenWriter<ApplicationUser> _tokenWriter;

    public AuthService(SignInManager<ApplicationUser> signInManager, ITokenWriter<ApplicationUser> tokenWriter)
    {
        _signInManager = signInManager;
        _tokenWriter = tokenWriter;
    }

    public async Task<string?> RegisterAsync(RegisterViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Name = viewModel.Name,
            LastName = viewModel.LastName,
            Birthdate = viewModel.Birthdate,
            UserName = viewModel.Email,
            Email = viewModel.Email,
            Password = viewModel.Password,
            Role = viewModel.Role,
        };
        
        var result = await _signInManager.UserManager.CreateAsync(user, user.Password);
        await _signInManager.UserManager.AddToRoleAsync(user, user.Role.ToString());
        if (!result.Succeeded)
        {
            return null;
        }

        var registerResult = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
        if (!registerResult.Succeeded)
        {
            return null;
        }

        string token = await _tokenWriter.WriteTokenAsync(user, cancellationToken);
        return token;
    }

    public async Task<string?> LoginAsync(LoginViewModel viewModel, CancellationToken cancellationToken = default)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, false);
        if (!signInResult.Succeeded)
        {
            return null;
        }

        var user = (await _signInManager.UserManager.FindByEmailAsync(viewModel.Email))!;
        string token = await _tokenWriter.WriteTokenAsync(user, cancellationToken);

        return token;
    }
}