using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IReceivedMessagesProcessor<T>
    {
        public Task ExecuteAsync(CancellationToken stoppingToken, Action<T> callback = null);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
