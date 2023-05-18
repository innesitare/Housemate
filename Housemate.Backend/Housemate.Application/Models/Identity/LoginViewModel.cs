namespace Housemate.Application.Models.Identity;

public sealed class LoginViewModel
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}