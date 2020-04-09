using AzureDeveloperTemplates.Starter.WebAPI.Configuration.Interfaces;

namespace AzureDeveloperTemplates.Starter.WebAPI.Configuration
{
    public class AzureAuthenticationConfiguration : IAzureAuthenticationConfiguration
    {
        public string AzureTenantId { get; set; }
        public string AzureClientId { get; set; }
        public string AzureClientSecret { get; set; }
    }
}
