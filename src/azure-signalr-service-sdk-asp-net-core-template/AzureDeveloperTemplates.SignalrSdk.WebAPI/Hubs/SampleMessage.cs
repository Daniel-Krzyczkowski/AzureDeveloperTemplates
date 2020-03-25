using System;

namespace AzureDeveloperTemplates.SignalrSdk.WebAPI.Hubs
{
    public class SampleMessage
    {
        public string MessageContent { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
