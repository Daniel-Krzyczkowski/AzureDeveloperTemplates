using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class StorageServiceConfiguration : IStorageServiceConfiguration
    {
        public string ContainerName { get; set; }
        public string ConnectionString { get; set; }
    }
}
