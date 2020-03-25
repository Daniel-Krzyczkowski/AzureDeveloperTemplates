using Azure.Security.KeyVault.Secrets;

namespace AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces
{
    public interface IKeyVaultSecretClientClientFactory
    {
        SecretClient SecretClient { get; }
    }
}
