using Amazon;
using Amazon.Runtime.CredentialManagement;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Housemate.Application.Extensions;

public static class AwsSecretsManagerExtensions
{
    public static IConfigurationBuilder AddAwsSecretsManager(this IConfigurationBuilder configurationBuilder)
    {
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string? applicationName = Assembly.GetEntryAssembly()?.GetName().Name;

        var chain = new CredentialProfileStoreChain();
        if (chain.TryGetAWSCredentials("Housemate", out var credentials))
        {
            return configurationBuilder
                .AddEnvironmentVariables()
                .AddSecretsManager(region: RegionEndpoint.EUCentral1, credentials: credentials, configurator: options =>
                {
                    options.SecretFilter = entry => entry.Name.StartsWith($"{environment}_{applicationName}");
                    options.KeyGenerator = (_, secretName) => secretName.Replace($"{environment}_{applicationName}_", string.Empty).Replace("__", ":");
                });
        }

        return configurationBuilder;
    }
}