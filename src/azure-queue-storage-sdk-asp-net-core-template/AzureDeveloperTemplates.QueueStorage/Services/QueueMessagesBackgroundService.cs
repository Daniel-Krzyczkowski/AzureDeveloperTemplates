using AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Services
{
    public class QueueMessagesBackgroundService : BackgroundService
    {
        private readonly IReceivedQueueMessagesProcessor _receivedQueueMessagesProcessor;
        private readonly ILogger<QueueMessagesBackgroundService> _logger;
        public QueueMessagesBackgroundService(IReceivedQueueMessagesProcessor receivedQueueMessagesProcessor,
                                                                    ILogger<QueueMessagesBackgroundService> logger)
        {
            _receivedQueueMessagesProcessor = receivedQueueMessagesProcessor;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            _receivedQueueMessagesProcessor.ExecuteAsync(stoppingToken, (obj) => _logger.LogInformation(obj));

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _receivedQueueMessagesProcessor.StartAsync(cancellationToken).ConfigureAwait(false);
            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _receivedQueueMessagesProcessor.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
