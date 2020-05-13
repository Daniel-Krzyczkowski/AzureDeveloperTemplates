using System.Threading.Tasks;

namespace AzureDeveloperTemplates.FunctionAppWithDependencyInjection.Services
{
    public interface IMailService
    {
        Task SendInvitation(string emailAddress);
    }
}
