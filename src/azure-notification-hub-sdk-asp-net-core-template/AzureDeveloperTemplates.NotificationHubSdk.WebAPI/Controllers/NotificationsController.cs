using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications;
using AzureDeveloperTemplates.NotificationHubSdk.Infrastructure.Services.PushNotifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.NotificationHubSdk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {

        private readonly IPushNotificationService _pushNotificationService;

        public NotificationsController(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] PushNotification notification)
        {
            await _pushNotificationService.SendNotification(notification);
            return Ok();
        }
    }
}
