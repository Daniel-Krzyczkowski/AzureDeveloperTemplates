using AzureDeveloperTemplates.BlobStorage.Infrastructure.Services;
using AzureDeveloperTemplates.BlobStorage.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.BlobStorage.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.BlobStorage.WebAPI.Extensions
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
                ConnectionString = configuration["BlobStorageSettings:ConnectionString"],
                ContainerName = configuration["BlobStorageSettings:ContainerName"]
            };

            services.AddSingleton(blobStorageSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IStorageService, StorageService>();
        }
    }
}
