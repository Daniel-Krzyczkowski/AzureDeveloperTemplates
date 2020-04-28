using System;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.RealTimeMessaging.Contract
{
    public class RealTimeMessage
    {
        public string MessageContent { get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
