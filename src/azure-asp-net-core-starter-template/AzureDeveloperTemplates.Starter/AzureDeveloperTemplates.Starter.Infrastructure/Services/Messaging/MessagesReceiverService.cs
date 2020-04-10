using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class MessagesReceiverService : IMessagesReceiverService
    {
        private readonly IMessagingServiceConfiguration _messagingServiceConfiguration;
        private readonly IMessageReceiver _messageReceiver;

        public MessagesReceiverService(IMessagingServiceConfiguration messagingServiceConfiguration)
        {
            _messagingServiceConfiguration = messagingServiceConfiguration;

            var connectionString = new ServiceBusConnectionStringBuilder(_messagingServiceConfiguration.ListenConnectionString);
            _messageReceiver = new MessageReceiver(connectionString.GetNamespaceConnectionString(),
                EntityNameHelper.FormatSubscriptionPath(_messagingServiceConfiguration.TopicName, _messagingServiceConfiguration.Subscription),
                ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);
        }

        public Task<Message> ReceiveMessageAsync() => _messageReceiver.ReceiveAsync();
        public Task<Message> ReceiveMessageAsync(TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(operationTimeout);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount) => _messageReceiver.ReceiveAsync(maxMessageCount);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(maxMessageCount, operationTimeout);
    }
}
