using Housemate.Application.Clients.Abstractions;
using Housemate.Application.Models.Weather;
using Housemate.Application.Models.Weather.Responses;
using Housemate.Application.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Housemate.Application.Services;

public sealed class WeatherService : IWeatherService
{
    private readonly IWeatherHttpClient _httpClient;
    private readonly string? _apiKey;

    public WeatherService(IWeatherHttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["WeatherKey"];
    }

    public async Task<WeatherCondition?> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken = default)
    {
        string url = GetWeatherUrl(city, isForecast: false);
        string responseContent = await _httpClient.GetStringAsync(url, cancellationToken);

        var weatherResponse = JsonConvert.DeserializeObject<WeatherConditionResponse>(responseContent);
        if (weatherResponse == null || weatherResponse.Forecast.Count <= 0)
        {
            return null;
        }

        var weatherData = new WeatherCondition
        {
            Temperature = weatherResponse.WeatherMainData.Temperature,
            Description = weatherResponse.Forecast[0].Description
        };

        return weatherData;
    }

    public async Task<List<WeatherForecast>?> GetWeatherForecastAsync(string city, CancellationToken cancellationToken = default)
    {
        string url = GetWeatherUrl(city, isForecast: true);
        string responseContent = await _httpClient.GetStringAsync(url, cancellationToken);

        var weatherResponse = JsonConvert.DeserializeObject<WeatherForecastResponse>(responseContent);
        if (weatherResponse == null || weatherResponse.ForecastList.Count <= 0)
        {
            return null;
        }

        return weatherResponse.ForecastList.Select(forecastData => new WeatherForecast
        {
            Date = DateTimeOffset.FromUnixTimeSeconds(forecastData.Date).DateTime,
            Condition = new WeatherCondition
            {
                Temperature = forecastData.WeatherMainData.Temperature,
                Description = forecastData.WeatherList.Count > 0 ? forecastData.WeatherList[0].Description : string.Empty
            }
        }).ToList();
    }

    private string GetWeatherUrl(string city, bool isForecast)
    {
        const string baseUrl = "https://api.openweathermap.org/data/2.5/";
        const string units = "metric";

        return $"{baseUrl}{(isForecast ? "forecast" : "weather")}?q={city}&appid={_apiKey}&units={units}";
    }
}