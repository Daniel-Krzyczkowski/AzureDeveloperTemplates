using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces
{
    public interface IMessagesReceiverService
    {
        Task ReceiveMessagesAsync();
    }
}
