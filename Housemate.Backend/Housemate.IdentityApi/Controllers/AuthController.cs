using Housemate.Application.Helpers;
using Housemate.Application.Models.Identity;
using Housemate.Application.Services.Abstractions;
using Housemate.Contracts.Requests.AuthRequests;
using Housemate.IdentityApi.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace Housemate.IdentityApi.Controllers;

[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost(ApiEndpoints.Auth.Register)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        string? token = await _authService.RegisterAsync(request.MapToViewModel(Role.User), cancellationToken);

        return token is not null
            ? Ok(token)
            : Unauthorized();
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        string? token = await _authService.LoginAsync(request.MapToViewModel(), cancellationToken);

        return token is not null
            ? Ok(token)
            : Unauthorized();
    }
}