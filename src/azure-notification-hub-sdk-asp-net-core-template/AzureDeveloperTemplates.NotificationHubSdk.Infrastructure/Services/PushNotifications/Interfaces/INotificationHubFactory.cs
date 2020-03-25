using Microsoft.Azure.NotificationHubs;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces
{
    public interface INotificationHubFactory
    {
        public NotificationHubClient NotificationHubClient { get; }
    }
}
