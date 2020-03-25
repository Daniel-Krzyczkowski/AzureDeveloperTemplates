using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Primitives;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services
{
    public class MessagesSenderService : IMessagesSenderService
    {
        private readonly ITopicClient _client;
        private readonly ServiceBusSettings _serviceBusSettings;

        public MessagesSenderService(ServiceBusSettings serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings;
            var token = TokenProvider.CreateSharedAccessSignatureTokenProvider(_serviceBusSettings.SharedAccessName,
                                                                            _serviceBusSettings.SharedAccessKey, TokenScope.Entity);
            _client = new TopicClient(_serviceBusSettings.ServiceBusNamespace, _serviceBusSettings.TopicName, token);
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
