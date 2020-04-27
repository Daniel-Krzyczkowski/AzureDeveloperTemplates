using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;
using AzureDeveloperTemplates.Starter.API.BackgroundServices;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class EventsServiceCollectionExtensions
    {
        public static IServiceCollection AddEventsService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var eventHubConfiguration = serviceProvider.GetRequiredService<IEventsServiceConfiguration>();

            var eventHubProducerClient = new EventHubProducerClient(eventHubConfiguration.ListenAndSendConnectionString,
                                                eventHubConfiguration.EventHubName);
            services.TryAddSingleton(eventHubProducerClient);

            var eventHubConsumerClient = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName,
                                                 eventHubConfiguration.ListenAndSendConnectionString,
                                                 eventHubConfiguration.EventHubName);
            services.TryAddSingleton(eventHubConsumerClient);

            services.TryAddSingleton<IEventsReceiverService, EventsReceiverService>();
            services.TryAddSingleton<IEventsSenderService, EventsSenderService>();
            services.TryAddSingleton<IReceivedEventsProcessor, ReceivedEventsProcessor>();
            services.TryAddSingleton<EventsBackgroundService>();
            return services;
        }
    }
}
