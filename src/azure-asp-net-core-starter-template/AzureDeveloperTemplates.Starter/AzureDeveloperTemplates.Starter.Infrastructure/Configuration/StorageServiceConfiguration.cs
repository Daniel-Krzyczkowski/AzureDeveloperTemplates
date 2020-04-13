using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class StorageServiceConfiguration : IStorageServiceConfiguration
    {
        public string ContainerName { get; set; }
        public string ConnectionString { get; set; }
    }

    public class StorageServiceConfigurationValidation : IValidateOptions<StorageServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, StorageServiceConfiguration options)
        {

            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure Storage Account is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
