using AzureDeveloperTemplates.Starter.Core.DomainModel;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        Task SetAsPremiumCustomer(Customer customer);
    }
}
