using AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events
{
    public class ReceivedEventsProcessor : IReceivedEventsProcessor
    {
        private readonly IEventsReceiverService _eventsReceiverService;
        private readonly ILogger<ReceivedEventsProcessor> _logger;

        public ReceivedEventsProcessor(IEventsReceiverService eventsReceiverService,
                                                                    ILogger<ReceivedEventsProcessor> logger)
        {
            _eventsReceiverService = eventsReceiverService;
            _logger = logger;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken, Action<string> callback = null)
        {
            stoppingToken.Register(() =>
                _logger.LogInformation($"{nameof(ReceivedEventsProcessor)} background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _eventsReceiverService.ReceiveEventsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "A problem occurred while invoking a callback method");
                }
            }
            _logger.LogInformation(stoppingToken.IsCancellationRequested.ToString());
        }
    }
}
