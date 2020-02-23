using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces
{
    public interface IServiceBusClientFactory
    {
        public TopicClient TopicClient { get; }
    }
}
