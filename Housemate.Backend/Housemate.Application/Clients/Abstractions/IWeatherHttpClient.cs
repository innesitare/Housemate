namespace Housemate.Application.Clients.Abstractions;

public interface IWeatherHttpClient
{
    Task<string> GetStringAsync(string url, CancellationToken cancellationToken);
}