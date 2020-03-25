using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.SignalrSdk.WebAPI.Hubs
{
    [Authorize]
    public class SampleHub : Hub
    {
        [HubMethodName("SendDirectMessageToUser")]
        public async Task SendDirectMessageToUser(string sampleMessageAsJson)
        {
            var sampleMessage = JsonConvert.DeserializeObject<SampleMessage>(sampleMessageAsJson);

            sampleMessage.SenderId = new Guid(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var messageAsJson = JsonConvert.SerializeObject(sampleMessage);

            await Clients.User(sampleMessage.ReceiverId.ToString()).SendAsync(messageAsJson);
        }
    }
}
