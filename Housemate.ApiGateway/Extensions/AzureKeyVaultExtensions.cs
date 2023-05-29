using Azure.Identity;
using Housemate.ApiGateway.Providers;

namespace Housemate.ApiGateway.Extensions;

public static class AzureKeyVaultExtensions
{
    public static IConfigurationBuilder AddAzureKeyVault(this IConfigurationBuilder configuration)
    {
        return configuration.AddEnvironmentVariables()
            .AddAzureKeyVault(new Uri($"https://{Environment.GetEnvironmentVariable("AZURE_VAULT_NAME")}.vault.azure.net/"),
                new EnvironmentCredential(),
                new PrefixKeyVaultSecretManager("Housemate"));
    }
}