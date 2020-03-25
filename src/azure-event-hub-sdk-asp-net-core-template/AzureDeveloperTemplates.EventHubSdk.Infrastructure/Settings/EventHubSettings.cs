using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.EventHubSdk.Infrastructure.Settings
{
    public class EventHubSettings
    {
        public string ListenConnectionString { get; set; }
        public string SendConnectionString { get; set; }
        public string EventHubName { get; set; }
    }
}
