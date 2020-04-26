using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class ReceivedMessagesProcessor<T> : IReceivedMessagesProcessor<T>
    {
        private readonly IMessagesReceiverService _messagesReceiverService;
        private readonly IDeserializerFactory<T> _deserializerFactory;
        private readonly ILogger<ReceivedMessagesProcessor<T>> _logger;
        private readonly IList<Exception> _exceptions;

        public ReceivedMessagesProcessor(IMessagesReceiverService messagesReceiverService,
                                                                    IDeserializerFactory<T> deserializerFactory,
                                                                    ILogger<ReceivedMessagesProcessor<T>> logger)
        {
            _messagesReceiverService = messagesReceiverService;
            _deserializerFactory = deserializerFactory;
            _logger = logger;
            _exceptions = new List<Exception>();
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken, Action<T> callback = null)
        {
            if (callback == null)
            {
                callback = (obj) => _logger.LogInformation(JsonConvert.SerializeObject(obj));
            }

            stoppingToken.Register(() =>
                _logger.LogInformation($"{nameof(ReceivedMessagesProcessor<T>)} background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var message = await _messagesReceiverService.ReceiveMessageAsync();
                    if (message != null)
                    {
                        var body = message.Body.ToArray();
                        callback(_deserializerFactory.Deserialize(message.ContentType, body));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "A problem occurred while invoking a callback method");
                    _exceptions.Add(ex);
                }

                await Task.Delay(5000);
            }
            _logger.LogInformation(stoppingToken.IsCancellationRequested.ToString());
            _logger.LogInformation($"{nameof(ReceivedMessagesProcessor<T>)} background task is stopping.");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_exceptions.Any())
            {
                _logger.LogCritical(new AggregateException(_exceptions), "The host threw exceptions unexpectedly");
            }
            await _messagesReceiverService.DisposeAsync();
        }
    }
}
