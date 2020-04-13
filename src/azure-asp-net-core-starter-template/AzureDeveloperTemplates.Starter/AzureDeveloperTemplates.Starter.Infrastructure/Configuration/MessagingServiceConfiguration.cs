using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class MessagingServiceConfiguration : IMessagingServiceConfiguration
    {
        public string TopicName { get; set; }
        public string Subscription { get; set; }
        public string ServiceBusNamespace { get; set; }
        public string ListenConnectionString { get; set; }
        public string SendConnectionString { get; set; }
    }

    public class MessagingServiceConfigurationValidation : IValidateOptions<MessagingServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, MessagingServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ListenConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ListenConnectionString)} configuration parameter for the Azure Service Bus is required");
            }

            if (string.IsNullOrEmpty(options.SendConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.SendConnectionString)} configuration parameter for the Azure Service Bus is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
