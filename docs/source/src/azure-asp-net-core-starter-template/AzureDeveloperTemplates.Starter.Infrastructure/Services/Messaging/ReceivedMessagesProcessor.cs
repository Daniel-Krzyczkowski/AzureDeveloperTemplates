using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Contract;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class ReceivedMessagesProcessor<T> : IReceivedMessagesProcessor<T>
    {
        private readonly IMessagesReceiverService _messagesReceiverService;
        private readonly IDeserializerFactory<T> _deserializerFactory;
        private readonly ILogger<ReceivedMessagesProcessor<T>> _logger;

        public ReceivedMessagesProcessor(IMessagesReceiverService messagesReceiverService,
                                                                    IDeserializerFactory<T> deserializerFactory,
                                                                    ILogger<ReceivedMessagesProcessor<T>> logger)
        {
            _messagesReceiverService = messagesReceiverService;
            _deserializerFactory = deserializerFactory;
            _logger = logger;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken, Action<T> callback = null)
        {
            if (callback == null)
            {
                callback = (obj) => _logger.LogInformation(JsonConvert.SerializeObject(obj));
            }

            using (stoppingToken.Register(() =>
                  _logger.LogInformation($"{nameof(ReceivedMessagesProcessor<T>)} background task is stopping."))) ;

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var message = await _messagesReceiverService.ReceiveMessageAsync(TimeSpan.FromSeconds(5), stoppingToken);
                    if (message != null)
                    {
                        var body = message.Body.ToArray();
                        callback(_deserializerFactory.Deserialize(message.ContentType, body));
                    }
                }

                catch (Exception ex)
                {
                    _logger.LogError(ex, "A problem occurred while invoking a callback method");
                }
            }

            _logger.LogInformation($"Cancellation token was requested");
        }
    }
}
