namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces
{
    public interface IDataServiceConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ContainerName { get; set; }
        string PartitionKeyPath { get; set; }
    }
}
