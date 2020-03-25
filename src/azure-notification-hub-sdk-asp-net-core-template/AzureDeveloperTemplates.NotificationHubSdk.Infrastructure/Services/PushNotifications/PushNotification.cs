using System.Collections.Generic;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications
{
    public class PushNotification
    {
        public string Message { get; set; }
        public IList<string> Tags { get; set; }
        public MobilePlatform MobilePlatform { get; set; }
    }
}
