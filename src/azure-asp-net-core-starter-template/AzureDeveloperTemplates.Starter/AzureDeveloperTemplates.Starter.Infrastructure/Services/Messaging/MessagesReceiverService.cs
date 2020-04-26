using Azure.Messaging.ServiceBus;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class MessagesReceiverService : IMessagesReceiverService
    {
        private readonly IMessagingServiceConfiguration _messagingServiceConfiguration;
        private readonly ServiceBusReceiver _serviceBusReceiver;
        private readonly ILogger<MessagesReceiverService> _logger;

        public MessagesReceiverService(IMessagingServiceConfiguration messagingServiceConfiguration,
                                       ServiceBusReceiver serviceBusReceiver,
                                       ILogger<MessagesReceiverService> logger)
        {
            _messagingServiceConfiguration = messagingServiceConfiguration;
            _serviceBusReceiver = serviceBusReceiver;
            _logger = logger;
        }

        public async Task<ServiceBusReceivedMessage> ReceiveMessageAsync(CancellationToken stoppingToken)
        {
            try
            {
                var message = await _serviceBusReceiver.ReceiveAsync();
                return message;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan operationTimeout, CancellationToken stoppingToken)
        {
            try
            {
                var message = await _serviceBusReceiver.ReceiveAsync(operationTimeout, stoppingToken);
                return message;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IList<ServiceBusReceivedMessage>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout, CancellationToken stoppingToken)
        {
            try
            {
                var messages = await _serviceBusReceiver.ReceiveBatchAsync(maxMessageCount, operationTimeout, stoppingToken);
                return messages;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }
    }
}
