using Microsoft.Azure.ServiceBus;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces
{
    public interface IServiceBusClientFactory
    {
        public TopicClient TopicClient { get; }
    }
}
