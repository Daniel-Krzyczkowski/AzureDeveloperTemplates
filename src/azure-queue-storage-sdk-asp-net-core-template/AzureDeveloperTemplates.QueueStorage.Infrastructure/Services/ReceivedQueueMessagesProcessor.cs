using AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Infrastructure.Services
{
    public class ReceivedQueueMessagesProcessor : IReceivedQueueMessagesProcessor
    {
        private readonly IQueueMessagesReceiverService _queueMessagesReceiverService;
        private readonly ILogger<ReceivedQueueMessagesProcessor> _logger;
        private readonly IList<Exception> _exceptions;

        public ReceivedQueueMessagesProcessor(IQueueMessagesReceiverService queueMessagesReceiverService,
                                                            ILogger<ReceivedQueueMessagesProcessor> logger)
        {
            _queueMessagesReceiverService = queueMessagesReceiverService;
            _logger = logger;
            _exceptions = new List<Exception>();
        }
        public async Task ExecuteAsync(CancellationToken stoppingToken, Action<string> callback = null)
        {
            stoppingToken.Register(() =>
                _logger.LogInformation($"{nameof(ReceivedQueueMessagesProcessor)} background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _queueMessagesReceiverService.ReceiveQueueMessagesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "A problem occurred while invoking a callback method");
                    _exceptions.Add(ex);
                }
            }
            _logger.LogInformation(stoppingToken.IsCancellationRequested.ToString());
            _logger.LogInformation($"{nameof(ReceivedQueueMessagesProcessor)} background task is stopping.");
        }

        private void _queueMessagesReceiverService_NewQueueMessageReceived(object sender, string e)
        {
            _logger.LogInformation(e);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _queueMessagesReceiverService.NewQueueMessageReceived += _queueMessagesReceiverService_NewQueueMessageReceived; ;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_exceptions.Any())
            {
                _logger.LogCritical(new AggregateException(_exceptions), "The host threw exceptions unexpectedly");
            }
            _queueMessagesReceiverService.NewQueueMessageReceived -= _queueMessagesReceiverService_NewQueueMessageReceived;
            return Task.CompletedTask;
        }
    }
}
