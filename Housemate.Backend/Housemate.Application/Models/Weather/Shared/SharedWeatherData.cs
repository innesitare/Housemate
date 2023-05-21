using Newtonsoft.Json;

namespace Housemate.Application.Models.Weather.Shared;

public sealed record WeatherMainData(
    [JsonProperty("temp")] double Temperature,
    [JsonProperty("feels_like")] double FeelsLike,
    [JsonProperty("temp_min")] double MinTemperature,
    [JsonProperty("temp_max")] double MaxTemperature,
    [JsonProperty("pressure")] int Pressure,
    [JsonProperty("humidity")] int Humidity
);

public sealed record SystemData(
    [JsonProperty("type")] int Type,
    [JsonProperty("id")] int Id,
    [JsonProperty("country")] string Country,
    [JsonProperty("sunrise")] int Sunrise,
    [JsonProperty("sunset")] int Sunset
);

public sealed record WeatherData(
    [JsonProperty("id")] int Id,
    [JsonProperty("main")] string WeatherMain,
    [JsonProperty("description")] string Description,
    [JsonProperty("icon")] string Icon
);

public sealed record WindData(
    [JsonProperty("speed")] double Speed,
    [JsonProperty("deg")] int Deg,
    [JsonProperty("gust")] double Gust
);

public sealed record Coordinates(
    [JsonProperty("lon")] double Longitude,
    [JsonProperty("lat")] double Latitude
);
    
public sealed record CloudsData(
    [JsonProperty("all")] int Coverage
);