using Housemate.Application.Models.Weather.Shared;
using Newtonsoft.Json;

namespace Housemate.Application.Models.Weather.Responses;

public sealed class WeatherConditionResponse
{
    [JsonProperty("weather")]
    public required List<WeatherData> Forecast { get; init; } = new List<WeatherData>();
    
    [JsonProperty("main")]
    public required WeatherMainData WeatherMainData { get; init; }

    [JsonProperty("wind")]
    public required WindData WindData { get; init; }
    
    [JsonProperty("clouds")]
    public required CloudsData CloudsData { get; init; }
    
    [JsonProperty("sys")]
    public required SystemData SystemData { get; init; }
    
    [JsonProperty("coord")]
    public required Coordinates Coordinates { get; init; }

    [JsonProperty("base")]
    public required string Base { get; init; }
    
    [JsonProperty("id")]
    public required int Id { get; init; }

    [JsonProperty("name")]
    public required string Name { get; init; }

    [JsonProperty("visibility")]
    public required int Visibility { get; init; }
    
    [JsonProperty("timezone")]
    public required int Timezone { get; init; }

    [JsonProperty("dt")]
    public required int Date { get; init; }

    [JsonProperty("cod")]
    public required int Cod { get; init; }
}