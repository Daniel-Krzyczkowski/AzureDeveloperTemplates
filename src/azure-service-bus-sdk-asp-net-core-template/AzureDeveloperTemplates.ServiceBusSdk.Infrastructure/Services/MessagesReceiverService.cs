using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services
{
    public class MessagesReceiverService : IMessagesReceiverService
    {
        private readonly ServiceBusSettings _serviceBusSettings;
        private readonly IMessageReceiver _messageReceiver;

        public MessagesReceiverService(ServiceBusSettings serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings;

            var connectionString = new ServiceBusConnectionStringBuilder(_serviceBusSettings.ConnectionString);
            _messageReceiver = new MessageReceiver(connectionString.GetNamespaceConnectionString(),
                EntityNameHelper.FormatSubscriptionPath(_serviceBusSettings.TopicName, _serviceBusSettings.Subscription),
                ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);
        }

        public Task<Message> ReceiveMessageAsync() => _messageReceiver.ReceiveAsync();
        public Task<Message> ReceiveMessageAsync(TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(operationTimeout);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount) => _messageReceiver.ReceiveAsync(maxMessageCount);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(maxMessageCount, operationTimeout);
    }
}
