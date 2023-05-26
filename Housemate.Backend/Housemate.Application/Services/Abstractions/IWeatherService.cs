using Housemate.Application.Models.Weather;

namespace Housemate.Application.Services.Abstractions;

public interface IWeatherService
{
    Task<WeatherCondition?> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken = default);
    
    Task<List<WeatherForecast>?> GetWeatherForecastAsync(string city, CancellationToken cancellationToken = default);
}