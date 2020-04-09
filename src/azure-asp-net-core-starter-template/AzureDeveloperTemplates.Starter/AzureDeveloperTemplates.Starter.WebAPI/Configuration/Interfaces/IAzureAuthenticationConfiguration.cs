namespace AzureDeveloperTemplates.Starter.WebAPI.Configuration.Interfaces
{
    public interface IAzureAuthenticationConfiguration
    {
        string AzureTenantId { get; set; }
        string AzureClientId { get; set; }
        string AzureClientSecret { get; set; }
    }
}
