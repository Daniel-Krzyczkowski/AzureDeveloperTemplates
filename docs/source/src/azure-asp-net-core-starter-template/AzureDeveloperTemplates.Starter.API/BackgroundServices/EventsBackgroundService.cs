using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.BackgroundServices
{
    public class EventsBackgroundService : BackgroundService
    {
        private readonly IReceivedEventsProcessor _receivedEventsProcessor;
        private readonly ILogger<EventsBackgroundService> _logger;
        public EventsBackgroundService(IReceivedEventsProcessor receivedEventsProcessor,
                                                                    ILogger<EventsBackgroundService> logger)
        {
            _receivedEventsProcessor = receivedEventsProcessor;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            _receivedEventsProcessor.ExecuteAsync(stoppingToken, (obj) => _logger.LogInformation(obj));
    }
}
