using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class DataServiceConfiguration : IDataServiceConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
        public string PartitionKeyPath { get; set; }
    }
}
