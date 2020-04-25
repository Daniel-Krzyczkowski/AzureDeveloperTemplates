using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Configuration
{
    public class MessagingServiceConfiguration : IMessagingServiceConfiguration
    {
        public string TopicName { get; set; }
        public string Subscription { get; set; }
        public string ServiceBusNamespace { get; set; }
        public string ListenAndSendConnectionString { get; set; }
    }

    public class MessagingServiceConfigurationValidation : IValidateOptions<MessagingServiceConfiguration>
    {
        public ValidateOptionsResult Validate(string name, MessagingServiceConfiguration options)
        {
            if (string.IsNullOrEmpty(options.ListenAndSendConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ListenAndSendConnectionString)} configuration parameter for the Azure Service Bus is required");
            }

            if (string.IsNullOrEmpty(options.ServiceBusNamespace))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.ServiceBusNamespace)} configuration parameter for the Azure Service Bus is required");
            }

            if (string.IsNullOrEmpty(options.Subscription))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.Subscription)} configuration parameter for the Azure Service Bus is required");
            }

            if (string.IsNullOrEmpty(options.TopicName))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.TopicName)} configuration parameter for the Azure Service Bus is required");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
