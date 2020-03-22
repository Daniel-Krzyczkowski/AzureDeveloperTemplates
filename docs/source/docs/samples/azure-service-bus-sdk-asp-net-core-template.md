[Azure Service Bus SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-service-bus-sdk-asp-net-core-template)

Sample project to present how to use Azure Service Bus SDK to send and receive messages.
Many thanks to [@HaraczPawel](https://twitter.com/HaraczPawel) who helped create this sample basing on the sample from his original [repository](https://github.com/PawelHaracz/pawelharacz.com)!

#### Packages used:
1. [Microsoft.Azure.ServiceBus](https://www.nuget.org/packages/Microsoft.Azure.ServiceBus/)

#### Code sample preview:

```csharp
    public class MessagesReceiverService : IMessagesReceiverService
    {
        private readonly ServiceBusSettings _serviceBusSettings;
        private readonly IMessageReceiver _messageReceiver;

        public MessagesReceiverService(ServiceBusSettings serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings;

            var connectionString = new ServiceBusConnectionStringBuilder(_serviceBusSettings.ConnectionString);
            _messageReceiver = new MessageReceiver(connectionString.GetNamespaceConnectionString(),
                EntityNameHelper.FormatSubscriptionPath(_serviceBusSettings.TopicName, _serviceBusSettings.Subscription),
                ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);
        }

        public Task<Message> ReceiveMessageAsync() => _messageReceiver.ReceiveAsync();
        public Task<Message> ReceiveMessageAsync(TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(operationTimeout);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount) => _messageReceiver.ReceiveAsync(maxMessageCount);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(maxMessageCount, operationTimeout);
    }
```

```csharp
    public class MessagesSenderService : IMessagesSenderService
    {
        private readonly ITopicClient _client;
        private readonly ServiceBusSettings _serviceBusSettings;

        public MessagesSenderService(ServiceBusSettings serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings;
            var token = TokenProvider.CreateSharedAccessSignatureTokenProvider(_serviceBusSettings.SharedAccessName,
                                                                            _serviceBusSettings.SharedAccessKey, TokenScope.Entity);
            _client = new TopicClient(_serviceBusSettings.ServiceBusNamespace, _serviceBusSettings.TopicName, token);
        }

        public async Task<string> SendMessageAsync(string messageBody)
        {
            var correlationId = Guid.NewGuid().ToString("N");
            var messageToSend = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));
            var message = new Message(messageToSend)
            {
                ContentType = $"{System.Net.Mime.MediaTypeNames.Application.Json};charset=utf-8",
                CorrelationId = correlationId
            };
            await _client.SendAsync(message);
            return correlationId;
        }
    }
```