using Azure.Messaging.ServiceBus;
using AzureDeveloperTemplates.Starter.API.BackgroundServices;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class MessagingServiceCollectionExtensions
    {
        public static IServiceCollection AddMessagingService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            var serviceBusConfiguration = serviceProvider.GetRequiredService<IMessagingServiceConfiguration>();

            services.TryAddSingleton(implementationFactory =>
            {
                var serviceBusClient = new ServiceBusClient(serviceBusConfiguration.ListenAndSendConnectionString);
                return serviceBusClient;
            });

            services.TryAddSingleton(implementationFactory =>
            {
                var serviceBusClient = implementationFactory.GetRequiredService<ServiceBusClient>();
                var serviceBusReceiver = serviceBusClient.CreateReceiver(serviceBusConfiguration.TopicName, serviceBusConfiguration.Subscription);
                return serviceBusReceiver;
            });

            services.TryAddSingleton(implementationFactory =>
            {
                var serviceBusClient = implementationFactory.GetRequiredService<ServiceBusClient>();
                var serviceBusSender = serviceBusClient.CreateSender(serviceBusConfiguration.TopicName);
                return serviceBusSender;
            });

            services.TryAddSingleton<IDeserializerFactory<object>, DeserializerFactory<object>>();
            services.TryAddSingleton<IMessagesReceiverService, MessagesReceiverService>();
            services.TryAddSingleton<IMessagesSenderService, MessagesSenderService>();
            services.TryAddSingleton<IDeserializer<object>, JsonUtf8Deserializer<object>>();
            services.TryAddSingleton<IReceivedMessagesProcessor<object>, ReceivedMessagesProcessor<object>>();
            services.AddHostedService<MessagingBackgroundService>();
            return services;
        }
    }
}
