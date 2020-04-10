using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging
{
    public class MessagesSenderService : IMessagesSenderService
    {
        private readonly ITopicClient _client;
        private readonly IMessagingServiceConfiguration _messagingServiceConfiguration;

        public MessagesSenderService(IMessagingServiceConfiguration messagingServiceConfiguration)
        {
            _messagingServiceConfiguration = messagingServiceConfiguration;
            _client = new TopicClient(_messagingServiceConfiguration.SendConnectionString, _messagingServiceConfiguration.TopicName);
        }

        public async Task<string> SendMessageAsync(string messageBody)
        {
            var correlationId = Guid.NewGuid().ToString("N");
            var messageToSend = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));
            var message = new Message(messageToSend)
            {
                ContentType = $"{System.Net.Mime.MediaTypeNames.Application.Json};charset=utf-8",
                CorrelationId = correlationId
            };
            await _client.SendAsync(message);
            return correlationId;
        }
    }
}
