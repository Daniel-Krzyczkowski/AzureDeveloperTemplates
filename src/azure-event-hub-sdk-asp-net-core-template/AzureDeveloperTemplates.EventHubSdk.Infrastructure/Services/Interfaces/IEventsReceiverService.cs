using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services.Interfaces
{
    public interface IEventsReceiverService
    {
        Task ReceiveEventsAsync(CancellationToken cancellationToken);
        event EventHandler<string> NewEventMessageReceived;
    }
}
