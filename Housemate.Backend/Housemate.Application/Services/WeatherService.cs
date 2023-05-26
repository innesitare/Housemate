using Housemate.Application.Clients.Abstractions;
using Housemate.Application.Models.Weather;
using Housemate.Application.Models.Weather.Responses;
using Housemate.Application.Services.Abstractions;
using Housemate.Application.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Housemate.Application.Services;

public sealed class WeatherService : IWeatherService
{
    private readonly IWeatherHttpClient _httpClient;
    private readonly IOptionsMonitor<OpenWeatherApiSettings> _weatherApiOptions;

    public WeatherService(IWeatherHttpClient httpClient, IOptionsMonitor<OpenWeatherApiSettings> weatherApiOptions)
    {
        _httpClient = httpClient;
        _weatherApiOptions = weatherApiOptions;
    }

    public async Task<WeatherCondition?> GetCurrentWeatherAsync(string city, CancellationToken cancellationToken = default)
    {
        string url = GetWeatherUrl(city, isForecast: false);
        var weatherResponse = await GetWeatherResponseAsync<WeatherConditionResponse>(url, cancellationToken);

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
        var weatherResponse = await GetWeatherResponseAsync<WeatherForecastResponse>(url, cancellationToken);

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
    
    private async Task<T?> GetWeatherResponseAsync<T>(string url, CancellationToken cancellationToken) where T : class
    {
        string responseContent = await _httpClient.GetStringAsync(url, cancellationToken);

        return JsonConvert.DeserializeObject<T>(responseContent);
    }

    private string GetWeatherUrl(string city, bool isForecast)
    {
        const string baseUrl = "https://api.openweathermap.org/data/2.5/";
        const string units = "metric";

        return $"{baseUrl}{(isForecast ? "forecast" : "weather")}?q={city}&appid={_weatherApiOptions.CurrentValue.ApiKey}&units={units}";
    }
}