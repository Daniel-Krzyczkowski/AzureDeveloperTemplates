using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Services.Interfaces
{
    public interface IMessagesSenderService
    {
        Task<string> SendMessageAsync(string messageBody);
    }
}
