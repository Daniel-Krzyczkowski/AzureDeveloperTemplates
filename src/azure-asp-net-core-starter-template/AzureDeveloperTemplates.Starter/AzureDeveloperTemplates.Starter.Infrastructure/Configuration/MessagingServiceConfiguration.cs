using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class MessagingServiceConfiguration : IMessagingServiceConfiguration
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
        public string Subscription { get; set; }
        public string ServiceBusNamespace { get; set; }
        public string SharedAccessName { get; set; }
        public string SharedAccessKey { get; set; }
    }
}
