using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDataService<Customer> _dataService;
        public CustomerService(IDataService<Customer> dataService)
        {
            _dataService = dataService;
        }

        public Task SetAsPremiumCustomer(Customer customer)
        {
            throw new System.NotImplementedException();
        }
    }
}
