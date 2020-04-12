using Azure.Cosmos;
using Azure.Storage.Blobs;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.WebAPI.Core.DependencyInjection
{
    public static class AzureClientServiceCollectionExtensions
    {
        public static IServiceCollection AddAzureServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();


            var storageConfiguration = serviceProvider.GetRequiredService<IStorageServiceConfiguration>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConfiguration.ConnectionString);
            services.TryAddSingleton(blobServiceClient);

            var cosmoDbConfiguration = serviceProvider.GetRequiredService<ICosmosDbDataServiceConfiguration>();
            CosmosClient cosmosClient = new CosmosClient(cosmoDbConfiguration.ConnectionString);
            services.TryAddSingleton(cosmosClient);

            return services;
        }
    }
}
