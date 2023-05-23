using Housemate.Application.Helpers;
using Housemate.Application.Models.Weather;
using Housemate.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Housemate.WeatherApi.Controllers;

[ApiController]
// [Authorize(Policy = "Bearer")]
public sealed class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    [HttpGet(ApiEndpoints.Weather.GetCurrent)]
    public async Task<ActionResult<WeatherCondition>> GetCurrentWeather(string city)
    {
        var weather = await _weatherService.GetCurrentWeatherAsync(city);
        if (weather is null)
        {
            return NotFound();
        }
        
        return Ok(weather);
    }

    [HttpGet(ApiEndpoints.Weather.GetForecast)]
    public async Task<ActionResult<List<WeatherForecast>>> GetWeatherForecast(string city)
    {
        var forecast = await _weatherService.GetWeatherForecastAsync(city);
        if (forecast is null)
        {
            return NotFound();
        }
        
        return Ok(forecast);
    }
}