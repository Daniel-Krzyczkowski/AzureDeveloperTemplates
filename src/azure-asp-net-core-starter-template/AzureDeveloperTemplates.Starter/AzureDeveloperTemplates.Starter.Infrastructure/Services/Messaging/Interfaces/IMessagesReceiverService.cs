using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IMessagesReceiverService
    {
        Task<ServiceBusReceivedMessage> ReceiveMessageAsync();
        Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan operationTimeout);
        Task<IList<ServiceBusReceivedMessage>> ReceiveMessageAsync(int maxMessageCount);
        Task<IList<ServiceBusReceivedMessage>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout);
    }
}
