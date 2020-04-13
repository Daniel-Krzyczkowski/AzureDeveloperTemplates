using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class CosmosDbDataServiceConfiguration : ICosmosDbDataServiceConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
        public string PartitionKeyPath { get; set; }
    }

    public class CosmosDbDataServiceConfigurationValidation : IValidateOptions<CosmosDbDataServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, CosmosDbDataServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Cosmos DB is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
