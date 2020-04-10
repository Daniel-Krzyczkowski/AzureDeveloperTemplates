namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IMessagingServiceConfiguration
    {
        string ListenConnectionString { get; set; }
        string SendConnectionString { get; set; }
        string TopicName { get; set; }
        string Subscription { get; set; }
        string ServiceBusNamespace { get; set; }
    }
}
