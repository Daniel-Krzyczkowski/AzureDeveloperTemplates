using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events
{
    public class EventsSenderService : IEventsSenderService
    {
        private readonly IEventsServiceConfiguration _eventHubServiceConfiguration;
        private readonly EventHubProducerClient _client;
        private readonly ILogger<EventsSenderService> _logger;

        public EventsSenderService(IEventsServiceConfiguration eventHubServiceConfiguration,
                                   EventHubProducerClient client,
                                   ILogger<EventsSenderService> logger)
        {
            _eventHubServiceConfiguration = eventHubServiceConfiguration;
            _client = client;
            _logger = logger;
        }

        public async Task SendEventAsync(string eventBody)
        {
            try
            {
                EventDataBatch eventBatch = await _client.CreateBatchAsync();
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventBody)));
                await _client.SendAsync(eventBatch);
            }

            catch (EventHubsException ex)
            {
                _logger.LogError($"{nameof(EventHubsException)} - error details: {ex.Message}");
                throw;
            }
        }
    }
}
