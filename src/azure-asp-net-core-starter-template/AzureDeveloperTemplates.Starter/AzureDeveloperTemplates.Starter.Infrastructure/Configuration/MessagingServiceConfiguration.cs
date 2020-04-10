using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class MessagingServiceConfiguration : IMessagingServiceConfiguration
    {
        public string TopicName { get; set; }
        public string Subscription { get; set; }
        public string ServiceBusNamespace { get; set; }
        public string ListenConnectionString { get; set; }
        public string SendConnectionString { get; set; }
    }
}
