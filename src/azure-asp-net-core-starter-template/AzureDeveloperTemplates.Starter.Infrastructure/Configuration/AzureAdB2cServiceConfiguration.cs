using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class AzureAdB2cServiceConfiguration : IAzureAdB2cServiceConfiguration
    {
        public string Tenant { get; set; }
        public string ClientId { get; set; }
        public string Policy { get; set; }
    }

    public class AzureAdB2cServiceConfigurationValidation : IValidateOptions<AzureAdB2cServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, AzureAdB2cServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ClientId))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ClientId)} configuration parameter for the Azure AD B2C is required");
            }

            if (string.IsNullOrEmpty(options.Policy))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.Policy)} configuration parameter for the Azure AD B2C is required");
            }

            if (string.IsNullOrEmpty(options.Tenant))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.Tenant)} configuration parameter for the Azure AD B2C is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
