using Azure.Storage.Queues;
using AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.QueueStorage.Infrastructure.Settings;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Infrastructure.Services
{
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
}
