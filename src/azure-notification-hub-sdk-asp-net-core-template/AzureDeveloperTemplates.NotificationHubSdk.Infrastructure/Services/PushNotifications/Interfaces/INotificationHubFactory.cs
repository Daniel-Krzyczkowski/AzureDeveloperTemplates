using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces
{
    public interface INotificationHubFactory
    {
        public NotificationHubClient NotificationHubClient { get; }
    }
}
