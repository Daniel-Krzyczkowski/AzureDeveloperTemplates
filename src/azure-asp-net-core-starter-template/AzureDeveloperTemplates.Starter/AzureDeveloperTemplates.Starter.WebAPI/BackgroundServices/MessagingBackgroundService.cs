using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.WebAPI.BackgroundServices
{
    internal class MessagingBackgroundService : BackgroundService
    {
        private readonly IReceivedMessagesProcessor<object> _receivedMessagesProcessor;
        private readonly ILogger<MessagingBackgroundService> _logger;
        public MessagingBackgroundService(IReceivedMessagesProcessor<object> receivedMessagesProcessor,
                                                                    ILogger<MessagingBackgroundService> logger)
        {
            _receivedMessagesProcessor = receivedMessagesProcessor;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
            _receivedMessagesProcessor.ExecuteAsync(stoppingToken, (obj) => _logger.LogInformation((JsonConvert.SerializeObject(obj))));

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await _receivedMessagesProcessor.StartAsync(cancellationToken).ConfigureAwait(false);
            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _receivedMessagesProcessor.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
