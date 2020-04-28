using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IMessagesReceiverService
    {
        Task<ServiceBusReceivedMessage> ReceiveMessageAsync(CancellationToken stoppingToken);
        Task<ServiceBusReceivedMessage> ReceiveMessageAsync(TimeSpan operationTimeout, CancellationToken stoppingToken);
        Task<IList<ServiceBusReceivedMessage>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout, CancellationToken stoppingToken);
    }
}
