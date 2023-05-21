using Housemate.Application.Models.Weather.Shared;
using Newtonsoft.Json;

namespace Housemate.Application.Models.Weather.Responses;

public sealed class WeatherForecastResponse
{
    [JsonProperty("list")]
    public required List<ForecastData> ForecastList { get; init; } = new List<ForecastData>();

    [JsonProperty("cod")]
    public required string Cod { get; init; }

    [JsonProperty("message")]
    public required double Message { get; init; }

    [JsonProperty("cnt")]
    public required int Count { get; init; }
}

public sealed record ForecastData
{
    [JsonProperty("weather")]
    public required List<WeatherData> WeatherList { get; init; } = new List<WeatherData>();
    
    [JsonProperty("main")]
    public required WeatherMainData WeatherMainData { get; init; }

    [JsonProperty("clouds")]
    public required CloudsData CloudsData { get; init; }

    [JsonProperty("wind")]
    public required WindData WindData { get; init; }
    
    [JsonProperty("sys")]
    public required SystemData SystemData { get; init; }
    
    [JsonProperty("dt")]
    public required long Date { get; init; }

    [JsonProperty("visibility")]
    public required int Visibility { get; init; }

    [JsonProperty("pop")]
    public required double ProbabilityOfPrecipitation { get; init; }

    [JsonProperty("dt_txt")]
    public required string DateText { get; init; }
}