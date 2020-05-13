using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class EventsServiceConfiguration : IEventsServiceConfiguration
    {
        public string ListenAndSendConnectionString { get; set; }
        public string EventHubName { get; set; }
    }

    public class EventsServiceConfigurationValidation : IValidateOptions<EventsServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, EventsServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.EventHubName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.EventHubName)} configuration parameter for the Azure Event Hub is required");
            }

            if (string.IsNullOrEmpty(options.ListenAndSendConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ListenAndSendConnectionString)} configuration parameter for the Azure Event Hub is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
