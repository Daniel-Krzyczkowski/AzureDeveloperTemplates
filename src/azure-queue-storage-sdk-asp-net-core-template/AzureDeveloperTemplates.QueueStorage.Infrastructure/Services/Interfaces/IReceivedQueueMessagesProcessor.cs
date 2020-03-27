using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces
{
    public interface IReceivedQueueMessagesProcessor
    {
        public Task ExecuteAsync(CancellationToken stoppingToken, Action<string> callback = null);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
