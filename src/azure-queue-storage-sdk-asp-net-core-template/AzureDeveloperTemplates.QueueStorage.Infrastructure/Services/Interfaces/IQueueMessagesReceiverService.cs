using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces
{
    public interface IQueueMessagesReceiverService
    {
        Task ReceiveQueueMessagesAsync(CancellationToken cancellationToken);
        event EventHandler<string> NewQueueMessageReceived;
    }
}
