using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events
{
    public class EventsReceiverService : IEventsReceiverService
    {
        private readonly EventHubConsumerClient _client;
        private readonly IEventsServiceConfiguration _eventHubServiceConfiguration;
        private readonly ILogger<EventsReceiverService> _logger;

        public EventsReceiverService(IEventsServiceConfiguration eventHubServiceConfiguration, ILogger<EventsReceiverService> logger)
        {
            _eventHubServiceConfiguration = eventHubServiceConfiguration;
            _client = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName,
                                                 _eventHubServiceConfiguration.ListenAndSendConnectionString,
                                                 _eventHubServiceConfiguration.EventHubName);
            _logger = logger;
        }

        public async Task ReceiveEventsAsync(CancellationToken cancellationToken)
        {
            try
            {
                await foreach (PartitionEvent partitionEvent in _client.ReadEventsAsync(cancellationToken))
                {
                    var eventMessage = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                };
            }

            catch (EventHubsException ex)
            {
                _logger.LogError($"{nameof(EventHubsException)} - error details: {ex.Message}");
                throw;
            }
        }
    }
}
