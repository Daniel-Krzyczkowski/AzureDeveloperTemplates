using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces
{
    public interface IPushNotificationService
    {
        Task<string> CreateRegistrationId(string handle);
        Task DeleteRegistration(string registrationId);
        Task RegisterForPushNotifications(string registrationId, DeviceRegistration deviceUpdate);
        Task<NotificationOutcome> SendNotification(PushNotification newNotification);
    }
}
