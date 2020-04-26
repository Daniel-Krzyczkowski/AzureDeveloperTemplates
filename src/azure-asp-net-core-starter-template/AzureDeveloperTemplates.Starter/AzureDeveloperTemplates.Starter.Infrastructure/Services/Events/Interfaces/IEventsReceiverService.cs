using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces
{
    public interface IEventsReceiverService
    {
        Task ReceiveEventsAsync(CancellationToken cancellationToken);
    }
}
