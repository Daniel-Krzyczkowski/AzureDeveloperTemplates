using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces
{
    public interface IEventsReceiverService : IAsyncDisposable
    {
        Task ReceiveEventsAsync(CancellationToken cancellationToken);
    }
}
