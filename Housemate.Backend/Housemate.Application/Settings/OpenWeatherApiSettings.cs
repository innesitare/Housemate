namespace Housemate.Application.Settings;

public sealed class OpenWeatherApiSettings
{
    public const string EnvironmentKey = "OpenWeatherMapApi";

    public required string ApiKey { get; init; }
}