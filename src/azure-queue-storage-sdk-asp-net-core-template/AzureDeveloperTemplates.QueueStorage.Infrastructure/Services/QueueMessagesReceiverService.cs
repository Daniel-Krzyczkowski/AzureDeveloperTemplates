using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.QueueStorage.Infrastructure.Settings;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Infrastructure.Services
{
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
}
