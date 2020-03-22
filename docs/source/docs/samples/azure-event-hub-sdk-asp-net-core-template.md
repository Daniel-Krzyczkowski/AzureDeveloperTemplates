[Azure Event Hub SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-event-hub-sdk-asp-net-core-template/AzureDeveloperTemplates)

Sample project to present how to use Azure Event Hub SDK to send and receive events using Azure Event Hub.

#### Packages used:
1. [Azure.Messaging.EventHubs](https://www.nuget.org/packages/Azure.Messaging.EventHubs/)

#### Code sample preview:

```csharp
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
```

```csharp
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
```