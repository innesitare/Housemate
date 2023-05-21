namespace Housemate.Application.Models.Weather;

public sealed class WeatherForecast
{
    public required DateTime Date { get; init; }
    
    public required WeatherCondition Condition { get; init; }
}