using Housemate.Application.Models.Identity;

namespace Housemate.Application.Services.Abstractions;

public interface IAuthService
{
    Task<string?> RegisterAsync(RegisterViewModel viewModel, CancellationToken cancellationToken = default);
    
    Task<string?> LoginAsync(LoginViewModel viewModel, CancellationToken cancellationToken = default);
}