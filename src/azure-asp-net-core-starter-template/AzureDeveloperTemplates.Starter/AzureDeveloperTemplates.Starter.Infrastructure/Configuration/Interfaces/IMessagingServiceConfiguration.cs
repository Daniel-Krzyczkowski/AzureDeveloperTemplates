namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IMessagingServiceConfiguration
    {
        string ConnectionString { get; set; }
        string TopicName { get; set; }
        string Subscription { get; set; }
        string ServiceBusNamespace { get; set; }
        string SharedAccessName { get; set; }
        string SharedAccessKey { get; set; }
    }
}
