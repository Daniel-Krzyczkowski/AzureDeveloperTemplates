using AzureDeveloperTemplates.QueueStorage.Infrastructure.Services;
using AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.QueueStorage.Infrastructure.Settings;
using AzureDeveloperTemplates.QueueStorage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDeveloperTemplates.QueueStorage.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionName = "QueueStorageSettings";
            var section = configuration.GetSection(sectionName);
            if (section.Exists() is false)
            {
                throw new ArgumentException($"Section {sectionName} does not exist");
            }

            var queueStorageSettings = new QueueStorageSettings()
            {
                ConnectionString = configuration["QueueStorageSettings:ConnectionString"],
                QueueName = configuration["QueueStorageSettings:QueueName"]
            };

            services.AddSingleton(queueStorageSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IQueueMessagesReceiverService, QueueMessagesReceiverService>();
            services.AddSingleton<IQueueMessagesSenderService, QueueMessagesSenderService>();
            services.AddSingleton<IReceivedQueueMessagesProcessor, ReceivedQueueMessagesProcessor>();
            services.AddHostedService<QueueMessagesBackgroundService>();
        }
    }
}
