namespace Housemate.Application.Models.Weather;

public sealed class WeatherCondition
{
    public required double Temperature { get; init; }
    
    public required string Description { get; init; }
}