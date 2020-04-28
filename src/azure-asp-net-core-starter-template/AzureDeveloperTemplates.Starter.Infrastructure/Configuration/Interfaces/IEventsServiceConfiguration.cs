namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IEventsServiceConfiguration
    {
        public string ListenAndSendConnectionString { get; set; }
        public string EventHubName { get; set; }
    }
}
