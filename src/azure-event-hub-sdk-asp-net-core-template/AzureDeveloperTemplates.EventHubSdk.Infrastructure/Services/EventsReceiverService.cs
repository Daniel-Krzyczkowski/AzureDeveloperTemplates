using Azure.Messaging.EventHubs.Consumer;
using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.EventHubSdk.Infrastructure.Settings;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services
{
    public class EventsReceiverService : IEventsReceiverService
    {
        private readonly EventHubConsumerClient _client;
        private readonly EventHubSettings _eventHubSettings;
        public event EventHandler<string> NewEventMessageReceived;

        public EventsReceiverService(EventHubSettings eventHubSettings)
        {
            _eventHubSettings = eventHubSettings;
            _client = new EventHubConsumerClient(EventHubConsumerClient.DefaultConsumerGroupName,
                                                 _eventHubSettings.ListenConnectionString,
                                                 _eventHubSettings.EventHubName);
        }

        public async Task ReceiveEventsAsync(CancellationToken cancellationToken)
        {
            await foreach (PartitionEvent partitionEvent in _client.ReadEventsAsync(cancellationToken))
            {
                var eventMessage = Encoding.UTF8.GetString(partitionEvent.Data.Body.ToArray());
                NewEventMessageReceived?.Invoke(this, eventMessage);
            };
        }
    }
}
