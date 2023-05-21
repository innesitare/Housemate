using Housemate.Application.Clients.Abstractions;

namespace Housemate.Application.Clients;

public sealed class WeatherHttpClient : IWeatherHttpClient
{
    private readonly HttpClient _httpClient;

    public WeatherHttpClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> GetStringAsync(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
        string content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

        return content;
    }
}