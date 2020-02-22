using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications
{
    public class DeviceRegistration
    {
        public MobilePlatform Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }
}
