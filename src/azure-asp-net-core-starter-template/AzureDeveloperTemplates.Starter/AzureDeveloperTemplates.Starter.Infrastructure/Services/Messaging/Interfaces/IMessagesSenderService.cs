using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Messaging.Interfaces
{
    public interface IMessagesSenderService
    {
        Task<string> SendMessageAsync(string messageBody);
    }
}
