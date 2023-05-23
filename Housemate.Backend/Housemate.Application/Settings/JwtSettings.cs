namespace Housemate.Application.Settings;

public sealed class JwtSettings
{
    public const string EnvironmentKey = "Jwt";
    
    public required string Audience { get; init; }
    
    public required string Issuer { get; init; }
    
    public required string Key { get; init; }
    
    public required double Expire { get; init; }
}