namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IMessagingServiceConfiguration
    {
        string ListenAndSendConnectionString { get; set; }
        string TopicName { get; set; }
        string Subscription { get; set; }
        string ServiceBusNamespace { get; set; }
    }
}
