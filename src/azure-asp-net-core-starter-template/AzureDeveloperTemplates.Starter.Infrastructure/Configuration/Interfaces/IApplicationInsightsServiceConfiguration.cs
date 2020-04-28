namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IApplicationInsightsServiceConfiguration
    {
        string InstrumentationKey { get; set; }
    }
}
