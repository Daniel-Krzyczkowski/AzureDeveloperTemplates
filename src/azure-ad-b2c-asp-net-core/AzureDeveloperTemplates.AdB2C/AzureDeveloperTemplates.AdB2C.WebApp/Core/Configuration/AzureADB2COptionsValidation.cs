using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.AdB2C.WebApp.Core.Configuration
{
    public class AzureADB2COptionsValidation : IValidateOptions<AzureADB2COptions>
    {
        public ValidateOptionsResult Validate(string name, AzureADB2COptions options)
        {
            if (string.IsNullOrEmpty(options.ClientId))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ClientId)} configuration parameter for the Azure AD B2C is required");
            }

            if (string.IsNullOrEmpty(options.ClientSecret))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ClientSecret)} configuration parameter for the Azure AD B2C is required");
            }

            if (string.IsNullOrEmpty(options.Domain))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.Domain)} configuration parameter for the Azure AD B2C is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
