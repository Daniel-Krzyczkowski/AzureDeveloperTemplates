namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IAzureAdB2cServiceConfiguration
    {
        string Tenant { get; set; }
        string ClientId { get; set; }
        string Policy { get; set; }
    }
}
