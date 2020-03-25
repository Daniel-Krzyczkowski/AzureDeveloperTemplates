namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings
{
    public class ServiceBusSettings
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
        public string Subscription { get; set; }
        public string ServiceBusNamespace { get; set; }
        public string SharedAccessName { get; set; }
        public string SharedAccessKey { get; set; }
    }
}
