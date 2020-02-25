using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Contracts;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings;
using AzureDeveloperTemplates.ServiceBusSdk.WebAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var sectionName = "ServiceBus";
            var section = configuration.GetSection(sectionName);
            if (section.Exists() is false)
            {
                throw new ArgumentException($"Section {sectionName} does not exist");
            }

            var serviceBusSettings = new ServiceBusSettings()
            {
                ConnectionString = configuration["ServiceBus:ConnectionString"],
                TopicName = configuration["ServiceBus:TopicName"],
                ServiceBusNamespace = configuration["ServiceBus:ServiceBusNamespace"],
                Subscription = configuration["ServiceBus:Subscription"],
                SharedAccessKey = configuration["ServiceBus:SharedAccessKey"],
                SharedAccessName = configuration["ServiceBus:SharedAccessName"]
            };

            services.AddSingleton(serviceBusSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDeserializerFactory<object>, DeserializerFactory<object>>();
            services.AddSingleton<IMessagesReceiverService, MessagesReceiverService>();
            services.AddSingleton<IMessagesSenderService, MessagesSenderService>();
            services.AddSingleton<IDeserializer<object>, JsonUtf8Deserializer<object>>();
            services.AddSingleton<IReceivedMessagesProcessor<object>, ReceivedMessagesProcessor<object>>();
            services.AddHostedService<MessagingBackgroundService>();
        }
    }
}
