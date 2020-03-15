using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Settings;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services
{
    public class EventsSenderService : IEventsSenderService
    {
        private readonly EventHubProducerClient _client;
        private readonly EventHubSettings _eventHubSettings;

        public EventsSenderService(EventHubSettings eventHubSettings)
        {
            _eventHubSettings = eventHubSettings;
            _client = new EventHubProducerClient(_eventHubSettings.SendConnectionString,
                                                 _eventHubSettings.EventHubName);
        }

        public async Task SendEventAsync(string eventBody)
        {
            EventDataBatch eventBatch = await _client.CreateBatchAsync();
            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventBody)));
            await _client.SendAsync(eventBatch);
        }
    }
}
