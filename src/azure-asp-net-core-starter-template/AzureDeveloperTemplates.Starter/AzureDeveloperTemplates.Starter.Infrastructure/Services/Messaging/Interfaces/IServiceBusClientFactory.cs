using Azure.Messaging.ServiceBus;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IServiceBusClientFactory
    {
        public ServiceBusClient ServiceBusClient { get; }
    }
}
