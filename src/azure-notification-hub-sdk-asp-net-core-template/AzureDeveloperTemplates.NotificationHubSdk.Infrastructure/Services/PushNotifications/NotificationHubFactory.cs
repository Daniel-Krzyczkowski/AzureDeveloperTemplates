using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces;
using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Settings;
using Microsoft.Azure.NotificationHubs;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications
{
    public class NotificationHubFactory : INotificationHubFactory
    {
        private readonly NotificationHubSettings _notificationHubSettings;

        public NotificationHubClient NotificationHubClient { get; }

        public NotificationHubFactory(NotificationHubSettings notificationHubSettings)
        {
            _notificationHubSettings = notificationHubSettings;
            NotificationHubClient = NotificationHubClient.CreateClientFromConnectionString(_notificationHubSettings.HubDefaultFullSharedAccessSignature,
                                                                       _notificationHubSettings.HubName);
        }
    }
}
