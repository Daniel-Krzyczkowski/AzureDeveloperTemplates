using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces
{
    public interface IMessagesReceiverService
    {
        Task<Message> ReceiveMessageAsync();
        Task<Message> ReceiveMessageAsync(TimeSpan operationTimeout);
        Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount);
        Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout);
    }
}
