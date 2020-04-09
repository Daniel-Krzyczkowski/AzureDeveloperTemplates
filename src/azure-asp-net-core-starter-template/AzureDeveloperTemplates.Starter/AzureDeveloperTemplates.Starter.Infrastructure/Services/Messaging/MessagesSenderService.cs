using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
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
            var token = TokenProvider.CreateSharedAccessSignatureTokenProvider(_messagingServiceConfiguration.SharedAccessName,
                                                                            _messagingServiceConfiguration.SharedAccessKey, TokenScope.Entity);
            _client = new TopicClient(_messagingServiceConfiguration.ServiceBusNamespace, _messagingServiceConfiguration.TopicName, token);
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
