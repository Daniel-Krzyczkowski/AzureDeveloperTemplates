using AzureDeveloperTemplates.Starter.Infrastructure.Configuration;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.WebAPI.Configuration;
using AzureDeveloperTemplates.Starter.WebAPI.Configuration.Interfaces;
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
            services.TryAddSingleton<IAzureAuthenticationConfiguration>(sp =>
                {
                    return new AzureAuthenticationConfiguration
                    {
                        AzureClientId = config["AZURE_CLIENT_ID"],
                        AzureClientSecret = config["AZURE_CLIENT_SECRET"],
                        AzureTenantId = config["AZURE_TENANT_ID"]
                    };
                });

            var test = services.BuildServiceProvider().GetRequiredService<IAzureAuthenticationConfiguration>();

            services.Configure<StorageServiceConfiguration>(config.GetSection("BlobStorageSettings"));
            services.TryAddSingleton<IStorageServiceConfiguration>(sp =>
                sp.GetRequiredService<IOptions<StorageServiceConfiguration>>().Value);

            services.Configure<MessagingServiceConfiguration>(config.GetSection("ServiceBusSettings"));
            services.TryAddSingleton<IMessagingServiceConfiguration>(sp =>
                sp.GetRequiredService<IOptions<MessagingServiceConfiguration>>().Value);

            return services;
        }
    }
}
