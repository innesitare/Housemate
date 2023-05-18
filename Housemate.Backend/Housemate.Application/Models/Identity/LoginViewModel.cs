namespace Housemate.Application.Models.Identity;

public sealed class LoginViewModel
{
    public required string Email { get; init; }

    public required string Password { get; init; }
}