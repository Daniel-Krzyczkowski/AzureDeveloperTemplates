namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface ISqlDbDataServiceConfiguration
    {
        string ConnectionString { get; set; }
    }
}
