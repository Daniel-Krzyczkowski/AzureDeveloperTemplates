using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services;
using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Settings;
using AzureDeveloperTemplates.EventHubSdk.WebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDeveloperTemplates.EventHubSdk.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionName = "EventHub";
            var section = configuration.GetSection(sectionName);
            if (section.Exists() is false)
            {
                throw new ArgumentException($"Section {sectionName} does not exist");
            }

            var eventHubSettings = new EventHubSettings()
            {
                ListenConnectionString = configuration["EventHub:ListenConnectionString"],
                SendConnectionString = configuration["EventHub:SendConnectionString"],
                EventHubName = configuration["EventHub:EventHubName"]
            };

            services.AddSingleton(eventHubSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IEventsReceiverService, EventsReceiverService>();
            services.AddSingleton<IEventsSenderService, EventsSenderService>();
            services.AddSingleton<IReceivedEventsProcessor, ReceivedEventsProcessor>();
            services.AddHostedService<EventsBackgroundService>();
        }
    }
}
