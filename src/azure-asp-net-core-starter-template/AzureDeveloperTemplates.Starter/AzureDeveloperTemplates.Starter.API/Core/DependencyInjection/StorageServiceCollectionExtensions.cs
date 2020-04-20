using Azure.Storage.Blobs;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class StorageServiceCollectionExtensions
    {
        public static IServiceCollection AddStorageServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var storageConfiguration = serviceProvider.GetRequiredService<IStorageServiceConfiguration>();
            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConfiguration.ConnectionString);
            services.TryAddSingleton(blobServiceClient);

            return services;
        }
    }
}
