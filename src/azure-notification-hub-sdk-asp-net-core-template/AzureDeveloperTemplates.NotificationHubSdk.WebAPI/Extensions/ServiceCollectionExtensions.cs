using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications;
using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces;
using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.NotificationHubSdk.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var notificationHubSettings = new NotificationHubSettings()
            {
                HubName = configuration["NotificationHub:HubName"],
                HubDefaultFullSharedAccessSignature = configuration["NotificationHub:HubDefaultFullSharedAccessSignature"]
            };

            services.AddSingleton(notificationHubSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<INotificationHubFactory, NotificationHubFactory>();
            services.AddSingleton<IPushNotificationService, PushNotificationService>();
        }
    }
}
