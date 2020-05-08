using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class SignalRServiceConfiguration : ISignalRServiceConfiguration
    {
        public string ConnectionString { get; set; }
    }

    public class SignalRServiceConfigurationValidation : IValidateOptions<SignalRServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, SignalRServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ConnectionString)} configuration parameter for the Azure SignalR Service is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
