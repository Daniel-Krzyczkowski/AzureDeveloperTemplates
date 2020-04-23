using Azure.Messaging.ServiceBus;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class MessagesReceiverService : IMessagesReceiverService
    {
        private readonly IMessagingServiceConfiguration _messagingServiceConfiguration;
        private readonly ILogger<MessagesReceiverService> _logger;

        public MessagesReceiverService(IMessagingServiceConfiguration messagingServiceConfiguration, ILogger<MessagesReceiverService> logger)
        {
            _messagingServiceConfiguration = messagingServiceConfiguration;
            _logger = logger;
        }

        public async Task<ServiceBusReceivedMessage> ReceiveMessageAsync()
        {
            try
            {
                await using var client = new ServiceBusClient(_messagingServiceConfiguration.ListenConnectionString);
                var receiver = client.CreateReceiver(_messagingServiceConfiguration.TopicName, _messagingServiceConfiguration.Subscription);
                var message = await receiver.ReceiveAsync();
                return message;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan operationTimeout)
        {
            try
            {
                await using var client = new ServiceBusClient(_messagingServiceConfiguration.ListenConnectionString);
                var receiver = client.CreateReceiver(_messagingServiceConfiguration.TopicName);
                var message = await receiver.ReceiveAsync(operationTimeout);
                return message;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IList<ServiceBusReceivedMessage>> ReceiveMessageAsync(int maxMessageCount)
        {
            try
            {
                await using var client = new ServiceBusClient(_messagingServiceConfiguration.ListenConnectionString);
                var receiver = client.CreateReceiver(_messagingServiceConfiguration.TopicName);
                var messages = await receiver.ReceiveBatchAsync(maxMessageCount);
                return messages;
            }

            catch (ServiceBusException ex)
            {
                _logger.LogError($"{nameof(ServiceBusException)} - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IList<ServiceBusReceivedMessage>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout)
        {
            try
            {
                await using var client = new ServiceBusClient(_messagingServiceConfiguration.ListenConnectionString);
                var receiver = client.CreateReceiver(_messagingServiceConfiguration.TopicName);
                var messages = await receiver.ReceiveBatchAsync(maxMessageCount, operationTimeout);
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
