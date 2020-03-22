[Azure SignalR Service SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-signalr-service-sdk-asp-net-core-template)

Sample project to present how to use SignalR Service to send real time messages.

#### Packages used:
1. [Microsoft.Azure.SignalR](https://www.nuget.org/packages/Microsoft.Azure.SignalR/)

#### Code sample preview:

```csharp
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
```