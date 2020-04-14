﻿using AzureDeveloperTemplates.Starter.Infrastructure.Configuration;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<StorageServiceConfiguration>(config.GetSection("BlobStorageSettings"));
            services.AddSingleton<IValidateOptions<StorageServiceConfiguration>, StorageServiceConfigurationValidation>();
            var storageServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<StorageServiceConfiguration>>().Value;
            services.AddSingleton<IStorageServiceConfiguration>(storageServiceConfiguration);

            services.Configure<MessagingServiceConfiguration>(config.GetSection("ServiceBusSettings"));
            services.AddSingleton<IValidateOptions<MessagingServiceConfiguration>, MessagingServiceConfigurationValidation>();
            var messagingServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MessagingServiceConfiguration>>().Value;
            services.AddSingleton<IMessagingServiceConfiguration>(messagingServiceConfiguration);

            services.Configure<CosmosDbDataServiceConfiguration>(config.GetSection("CosmosDbSettings"));
            services.AddSingleton<IValidateOptions<CosmosDbDataServiceConfiguration>, CosmosDbDataServiceConfigurationValidation>();
            var cosmosDbDataServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<CosmosDbDataServiceConfiguration>>().Value;
            services.AddSingleton<ICosmosDbDataServiceConfiguration>(cosmosDbDataServiceConfiguration);

            services.Configure<SqlDbDataServiceConfiguration>(config.GetSection("SqlDbSettings"));
            services.AddSingleton<IValidateOptions<SqlDbDataServiceConfiguration>, SqlDbDataServiceConfigurationValidation>();
            var sqlDbDataServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<SqlDbDataServiceConfiguration>>().Value;
            services.AddSingleton<ISqlDbDataServiceConfiguration>(sqlDbDataServiceConfiguration);

            return services;
        }
    }
}