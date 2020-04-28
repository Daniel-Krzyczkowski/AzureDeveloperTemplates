using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Events.Interfaces
{
    public interface IEventsSenderService
    {
        Task SendEventAsync(string eventBody);
    }
}
