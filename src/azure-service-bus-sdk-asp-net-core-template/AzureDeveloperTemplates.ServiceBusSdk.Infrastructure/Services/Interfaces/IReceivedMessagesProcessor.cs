using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces
{
    public interface IReceivedMessagesProcessor<T>
    {
        public Task ExecuteAsync(CancellationToken stoppingToken, Action<T> callback = null);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
