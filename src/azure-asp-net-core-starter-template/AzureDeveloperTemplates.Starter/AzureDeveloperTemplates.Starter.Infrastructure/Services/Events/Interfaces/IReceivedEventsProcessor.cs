using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces
{
    public interface IReceivedEventsProcessor
    {
        public Task ExecuteAsync(CancellationToken stoppingToken, Action<string> callback = null);
    }
}
