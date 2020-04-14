using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using AzureDeveloperTemplates.Starter.API.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class MessagingServiceCollectionExtensions
    {
        public static IServiceCollection AddMessagingService(this IServiceCollection services)
        {
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
