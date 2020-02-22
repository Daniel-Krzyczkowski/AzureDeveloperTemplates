using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.NotificationHubs.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly INotificationHubFactory _notificationHubFactory;
        public PushNotificationService(INotificationHubFactory notificationHubFactory)
        {
            _notificationHubFactory = notificationHubFactory;
        }

        public async Task<string> CreateRegistrationId(string handle)
        {
            var hub = _notificationHubFactory.NotificationHubClient;
            string newRegistrationId = null;

            if (handle != null)
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(handle, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
            {
                newRegistrationId = await hub.CreateRegistrationIdAsync();
            }

            return newRegistrationId;
        }

        public async Task DeleteRegistration(string registrationId)
        {
            await _notificationHubFactory.NotificationHubClient.DeleteRegistrationAsync(registrationId);
        }

        public async Task RegisterForPushNotifications(string registrationId, DeviceRegistration deviceUpdate)
        {
            var hub = _notificationHubFactory.NotificationHubClient;
            RegistrationDescription registrationDescription = null;

            switch (deviceUpdate.Platform)
            {
                case MobilePlatform.wns:
                    registrationDescription = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case MobilePlatform.apns:
                    registrationDescription = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case MobilePlatform.fcm:
                    registrationDescription = new FcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new ArgumentException("Please provide correct platform notification service name");
            }

            registrationDescription.RegistrationId = registrationId;
            if (deviceUpdate.Tags != null)
                registrationDescription.Tags = new HashSet<string>(deviceUpdate.Tags);

            try
            {
                await hub.CreateOrUpdateRegistrationAsync(registrationDescription);
            }
            catch (MessagingException exception)
            {
                System.Diagnostics.Debug.WriteLine("Unhandled exception was thrown during registration in the Azure Notification Hub:");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
        }

        public async Task<NotificationOutcome> SendNotification(PushNotification newNotification)
        {
            var hub = _notificationHubFactory.NotificationHubClient;

            try
            {
                NotificationOutcome outcome = null;

                switch (newNotification.MobilePlatform)
                {
                    case MobilePlatform.wns:
                        var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">"
                         + newNotification.Message + "</text></binding></visual></toast>";

                        if (newNotification.Tags == null)
                            outcome = await hub.SendWindowsNativeNotificationAsync(toast);
                        else
                            outcome = await hub.SendWindowsNativeNotificationAsync(toast, newNotification.Tags);
                        break;
                    case MobilePlatform.apns:
                        var alert = "{\"aps\":{\"alert\":\"" + newNotification.Message + "\"}}";

                        if (newNotification.Tags == null)
                            outcome = await hub.SendAppleNativeNotificationAsync(alert);
                        else
                            outcome = await hub.SendAppleNativeNotificationAsync(alert, newNotification.Tags);
                        break;
                    case MobilePlatform.fcm:
                        var notification = "{ \"data\" : {\"message\":\"" + newNotification.Message + "\"}}";

                        if (newNotification.Tags == null)
                            outcome = await hub.SendFcmNativeNotificationAsync(notification);
                        else
                            outcome = await hub.SendFcmNativeNotificationAsync(notification, newNotification.Tags);
                        break;
                }

                if (outcome != null)
                {
                    if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                        (outcome.State == NotificationOutcomeState.Unknown)))
                    {
                        return outcome;
                    }
                }

                System.Diagnostics.Debug.WriteLine("Notification was not sent due to issue. Please send again.");
                return null;
            }

            catch (MessagingException exception)
            {
                System.Diagnostics.Debug.WriteLine("Unhandled exception was thrown during sending notification with the Azure Notification Hub:");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                return null;
            }

            catch (ArgumentException exception)
            {
                System.Diagnostics.Debug.WriteLine("Unhandled exception was thrown during sending notification with the Azure Notification Hub:");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                return null;
            }
        }
    }
}
