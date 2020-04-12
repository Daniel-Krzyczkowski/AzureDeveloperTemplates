using Azure.Core;
using Azure.Cosmos;
using Azure.Identity;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.WebAPI.Configuration.Interfaces;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace AzureDeveloperTemplates.Starter.WebAPI.Core.DependencyInjection
{
    public static class AzureClientServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            services.AddAzureClients(builder =>
            {
                TokenCredential credential = new DefaultAzureCredential();
#if DEBUG
                var azureAuthenticationConfiguration = serviceProvider.GetRequiredService<IAzureAuthenticationConfiguration>();
                credential = new ClientSecretCredential(azureAuthenticationConfiguration.AzureTenantId,
                                                        azureAuthenticationConfiguration.AzureClientId,
                                                        azureAuthenticationConfiguration.AzureClientSecret);
#endif

                var storageConfiguration = serviceProvider.GetRequiredService<IStorageServiceConfiguration>();

                builder.AddBlobServiceClient(new Uri(storageConfiguration.BlobStorageUrl))
                       .ConfigureOptions((options, provider) =>
                       {
                           options.Retry.MaxRetries = 3;
                           options.Retry.Delay = TimeSpan.FromSeconds(3);
                           options.Diagnostics.IsLoggingEnabled = false;
                       });

                builder.UseCredential(credential);
            });

            var cosmoDbConfiguration = serviceProvider.GetRequiredService<ICosmosDbDataServiceConfiguration>();
            CosmosClient cosmosClient = new CosmosClient(cosmoDbConfiguration.ConnectionString);
            services.TryAddSingleton(cosmosClient);

            return services;
        }
    }
}
