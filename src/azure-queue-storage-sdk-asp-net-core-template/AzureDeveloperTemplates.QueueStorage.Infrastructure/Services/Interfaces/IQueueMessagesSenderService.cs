using System.Threading.Tasks;

namespace AzureDeveloperTemplates.QueueStorage.Infrastructure.Services.Interfaces
{
    public interface IQueueMessagesSenderService
    {
        Task SendEventAsync(string queueMessageBody);
    }
}
