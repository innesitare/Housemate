namespace Housemate.Application.Models.Identity;

public sealed class RegisterViewModel
{
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required string Password { get; init; }
    
    public required DateOnly Birthdate { get; init; }
    
    public required Role Role { get; init; }
}