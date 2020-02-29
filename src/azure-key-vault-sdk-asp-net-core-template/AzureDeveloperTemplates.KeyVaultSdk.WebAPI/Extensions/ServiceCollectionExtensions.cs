using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services;
using AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            var keyVaultSecretClientClientFactory = new KeyVaultSecretClientClientFactory(secretClient);
            return keyVaultSecretClientClientFactory;
        }
    }
}
