using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces
{
    public interface IEventsSenderService : IAsyncDisposable
    {
        Task SendEventAsync(string eventBody);
    }
}
