using AzureDeveloperTemplates.Starter.Infrastructure.Services.RealTimeMessaging.Contract;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.Hubs
{
    public class RealTimeMessageHub : Hub
    {
        [HubMethodName("direct-message")]
        public async Task SendDirectMessageToUser(string sampleMessageAsJson)
        {
            var sampleMessage = JsonConvert.DeserializeObject<RealTimeMessage>(sampleMessageAsJson);

            sampleMessage.SenderId = new Guid(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var messageAsJson = JsonConvert.SerializeObject(sampleMessage);

            await Clients.User(sampleMessage.ReceiverId.ToString()).SendAsync(messageAsJson);
        }
    }
}
