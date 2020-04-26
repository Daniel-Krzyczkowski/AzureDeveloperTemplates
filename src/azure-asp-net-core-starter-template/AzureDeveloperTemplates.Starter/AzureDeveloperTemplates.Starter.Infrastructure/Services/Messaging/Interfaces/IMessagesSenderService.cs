using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IMessagesSenderService : IAsyncDisposable
    {
        Task<string> SendMessageAsync(string messageBody);
    }
}
