using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services.Interfaces
{
    public interface IReceivedEventsProcessor
    {
        public Task ExecuteAsync(CancellationToken stoppingToken, Action<string> callback = null);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
