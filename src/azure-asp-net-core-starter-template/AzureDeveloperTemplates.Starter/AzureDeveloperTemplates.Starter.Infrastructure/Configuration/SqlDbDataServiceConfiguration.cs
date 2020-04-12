using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class SqlDbDataServiceConfiguration : ISqlDbDataServiceConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
