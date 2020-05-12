namespace AzureDeveloperTemplates.AdB2C.WebAPI.Core.Configuration.Interfaces
{
    public interface IAzureAdB2cServiceConfiguration
    {
        string Tenant { get; set; }
        string ClientId { get; set; }
        string Policy { get; set; }
    }
}
