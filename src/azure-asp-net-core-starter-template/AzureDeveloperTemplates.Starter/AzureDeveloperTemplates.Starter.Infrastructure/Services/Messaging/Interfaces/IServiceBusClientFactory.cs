using Microsoft.Azure.ServiceBus;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IServiceBusClientFactory
    {
        public TopicClient TopicClient { get; }
    }
}
