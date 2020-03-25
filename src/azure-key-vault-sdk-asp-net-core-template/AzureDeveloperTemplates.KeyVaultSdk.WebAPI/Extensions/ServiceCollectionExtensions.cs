using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services;
using AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDeveloperTemplates.KeyVaultSdk.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var keyVaultSecretClientClientFactory = InitializeSecretClientInstanceAsync(configuration);
            services.AddSingleton<IKeyVaultSecretClientClientFactory>(keyVaultSecretClientClientFactory);
            services.AddSingleton<ISecretManager, SecretManager>();
        }

        private static KeyVaultSecretClientClientFactory InitializeSecretClientInstanceAsync(IConfiguration configuration)
        {
            string keyVaultUrl = configuration["KeyVaultSettings:Url"];

            TokenCredential credential = new DefaultAzureCredential();
#if DEBUG
            credential = new ClientSecretCredential(configuration["AZURE_TENANT_ID"],
                                                    configuration["AZURE_CLIENT_ID"],
                                                    configuration["AZURE_CLIENT_SECRET"]);
#endif

            var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);
            var keyVaultSecretClientClientFactory = new KeyVaultSecretClientClientFactory(secretClient);
            return keyVaultSecretClientClientFactory;
        }
    }
}
