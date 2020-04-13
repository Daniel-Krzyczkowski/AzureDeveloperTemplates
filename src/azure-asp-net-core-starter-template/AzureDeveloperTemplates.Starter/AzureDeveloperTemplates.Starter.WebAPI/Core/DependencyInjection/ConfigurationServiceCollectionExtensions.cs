using AzureDeveloperTemplates.Starter.Infrastructure.Configuration;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.WebAPI.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageServiceConfiguration>(config.GetSection("BlobStorageSettings"));
            services.TryAddSingleton<IStorageServiceConfiguration>(sp =>
                sp.GetRequiredService<IOptions<StorageServiceConfiguration>>().Value);

            services.Configure<MessagingServiceConfiguration>(config.GetSection("ServiceBusSettings"));
            services.TryAddSingleton<IMessagingServiceConfiguration>(sp =>
                sp.GetRequiredService<IOptions<MessagingServiceConfiguration>>().Value);

            services.Configure<CosmosDbDataServiceConfiguration>(config.GetSection("CosmosDbSettings"));
            services.TryAddSingleton<ICosmosDbDataServiceConfiguration>(sp =>
                sp.GetRequiredService<IOptions<CosmosDbDataServiceConfiguration>>().Value);

            services.Configure<SqlDbDataServiceConfiguration>(config.GetSection("SqlDbSettings"));
            services.TryAddSingleton<ISqlDbDataServiceConfiguration>(sp =>
                sp.GetRequiredService<IOptions<SqlDbDataServiceConfiguration>>().Value);

            return services;
        }
    }
}
