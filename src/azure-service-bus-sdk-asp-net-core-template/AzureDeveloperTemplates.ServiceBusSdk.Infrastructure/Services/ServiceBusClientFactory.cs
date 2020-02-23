using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services
{
    public class ServiceBusClientFactory : IServiceBusClientFactory
    {
        private readonly ServiceBusSettings _serviceBusSettings;

        public ServiceBusClientFactory(ServiceBusSettings serviceBusSettings, TopicClient topicClient)
        {
            _serviceBusSettings = serviceBusSettings;
            TopicClient = new TopicClient(_serviceBusSettings.ConnectionString,
                                                                _serviceBusSettings.Topic);
        }

        public TopicClient TopicClient
        {
            get;
        }
    }
}
