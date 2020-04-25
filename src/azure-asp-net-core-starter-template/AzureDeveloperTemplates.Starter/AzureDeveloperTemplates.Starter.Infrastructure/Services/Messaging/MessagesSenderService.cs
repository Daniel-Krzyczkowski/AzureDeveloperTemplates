using Azure.Messaging.ServiceBus;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class MessagesSenderService : IMessagesSenderService
    {
        private readonly IMessagingServiceConfiguration _messagingServiceConfiguration;
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ILogger<MessagesSenderService> _logger;

        public MessagesSenderService(IMessagingServiceConfiguration messagingServiceConfiguration,
                                     ServiceBusClient serviceBusClient,
                                     ILogger<MessagesSenderService> logger)
        {
            _messagingServiceConfiguration = messagingServiceConfiguration;
            _serviceBusClient = serviceBusClient;
            _logger = logger;
        }

        public async Task<string> SendMessageAsync(string messageBody)
        {
            try
            {
                var sender = _serviceBusClient.CreateSender(_messagingServiceConfiguration.TopicName);
                var correlationId = Guid.NewGuid().ToString("N");
                var messageToSend = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));
                var message = new ServiceBusMessage(messageToSend)
                {
                    ContentType = $"{System.Net.Mime.MediaTypeNames.Application.Json};charset=utf-8",
                    CorrelationId = correlationId
                };
                await sender.SendAsync(message);
                return correlationId;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }
    }
}
