using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.BackgroundServices
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receivedMessagesProcessor.ExecuteAsync(stoppingToken, (obj) => _logger.LogInformation((JsonConvert.SerializeObject(obj))));
        }
    }
}
