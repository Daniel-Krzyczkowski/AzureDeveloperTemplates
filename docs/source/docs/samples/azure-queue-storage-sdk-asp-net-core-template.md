[Azure Storage Queues SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-queue-storage-sdk-asp-net-core-template/)

Sample project to present how to use Azure Storage Queues SDK to send and receive queue messages using Azure Storage Queues.

#### Packages used:
1. [Azure.Storage.Queues](https://www.nuget.org/packages/Azure.Storage.Queues/)

#### Code sample preview:

```csharp
    public class QueueMessagesReceiverService : IQueueMessagesReceiverService
    {
        public event EventHandler<string> NewQueueMessageReceived;
        private readonly QueueClient _client;
        private readonly QueueStorageSettings _queueStorageSettings;

        public QueueMessagesReceiverService(QueueStorageSettings queueStorageSettings)
        {
            _queueStorageSettings = queueStorageSettings;
            _client = new QueueClient(_queueStorageSettings.ConnectionString, _queueStorageSettings.QueueName);
        }


        public async Task ReceiveQueueMessagesAsync(CancellationToken cancellationToken)
        {
            var queueMessages = await _client.ReceiveMessagesAsync(cancellationToken);

            foreach (QueueMessage message in queueMessages.Value)
            {
                NewQueueMessageReceived?.Invoke(this, message.MessageText);

                _client.DeleteMessage(message.MessageId, message.PopReceipt);
            }
        }
    }
```

```csharp
    public class QueueMessagesSenderService : IQueueMessagesSenderService
    {
        private readonly QueueClient _client;
        private readonly QueueStorageSettings _queueStorageSettings;

        public QueueMessagesSenderService(QueueStorageSettings queueStorageSettings)
        {
            _queueStorageSettings = queueStorageSettings;
            _client = new QueueClient(_queueStorageSettings.ConnectionString, _queueStorageSettings.QueueName);
        }

        public async Task SendEventAsync(string queueMessageBody)
        {
            await _client.CreateIfNotExistsAsync();

            await _client.SendMessageAsync(queueMessageBody);
        }
    }
```