using AzureDeveloperTemplates.AdB2C.UWP.Services.Contract;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.AdB2C.UWP.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationData> Authenticate();
    }
}
