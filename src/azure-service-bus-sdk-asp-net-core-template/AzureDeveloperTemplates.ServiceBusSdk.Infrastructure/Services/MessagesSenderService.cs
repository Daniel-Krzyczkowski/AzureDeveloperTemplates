using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services
{
    public class MessagesSenderService : IMessagesSenderService
    {
        private readonly IServiceBusClientFactory _serviceBusClientFactory;

        public MessagesSenderService(IServiceBusClientFactory serviceBusClientFactory)
        {
            _serviceBusClientFactory = serviceBusClientFactory;
        }

        public async Task SendMessageToTopicAsync(string messageBody)
        {
            throw new NotImplementedException();
        }
    }
}
