using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
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
                ContainerName = configuration["BlobStorageSettings:ContainerName"]
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

                builder.AddSecretClient(new Uri(configuration.GetSection("KeyVaultSettings:Url").Value))
                        .ConfigureOptions(options => options.Retry.MaxRetries = 3)
                        .WithCredential(credential);

                var secretClient = services.BuildServiceProvider().GetService<SecretClient>();
                var secret = secretClient.GetSecret("BlobStorageConnectionString").Value;

                builder.AddBlobServiceClient(secret.Value)
                       .ConfigureOptions((options, provider) =>
                        {
                            options.Retry.MaxRetries = 10;
                            options.Retry.Delay = TimeSpan.FromSeconds(3);
                            options.Diagnostics.IsLoggingEnabled = true;
                            options.AddPolicy(provider.GetService<SimpleTracingPolicy>(), HttpPipelinePosition.PerCall);
                        });




                //Set global configuration for all clients:
                //builder.ConfigureDefaults(options => options.Retry.Mode = RetryMode.Exponential);

            });
        }
    }
}
