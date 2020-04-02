using Azure.Core;
using Azure.Identity;
using AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Policies;
using AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Settings;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionName = "BlobStorageSettings";
            var section = configuration.GetSection(sectionName);
            if (section.Exists() is false)
            {
                throw new ArgumentException($"Section {sectionName} does not exist");
            }

            var blobStorageSettings = new BlobStorageSettings()
            {
                ContainerName = configuration["BlobStorageSettings:ContainerName"],
                BlobStorageUrl = configuration["BlobStorageSettings:BlobStorageUrl"]
            };

            services.AddSingleton(blobStorageSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SimpleTracingPolicy>();

            services.AddAzureClients(builder =>
            {
                TokenCredential credential = new DefaultAzureCredential();
#if DEBUG
                credential = new ClientSecretCredential(configuration["AZURE_TENANT_ID"],
                                                        configuration["AZURE_CLIENT_ID"],
                                                        configuration["AZURE_CLIENT_SECRET"]);
#endif

                builder.AddBlobServiceClient(new Uri(configuration["BlobStorageSettings:BlobStorageUrl"]))
                       .ConfigureOptions((options, provider) =>
                        {
                            options.Retry.MaxRetries = 10;
                            options.Retry.Delay = TimeSpan.FromSeconds(3);
                            options.Diagnostics.IsLoggingEnabled = false;
                            options.Diagnostics.ApplicationId = "AzureDeveloperTemplates";
                            options.AddPolicy(provider.GetService<SimpleTracingPolicy>(), HttpPipelinePosition.PerCall);
                        });
            });
        }
    }
}
