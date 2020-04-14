using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataService<Product> _dataService;
        public ProductService(IDataService<Product> dataService)
        {
            _dataService = dataService;
        }

        public Task AddToBasket(Product product)
        {
            throw new System.NotImplementedException();
        }
    }
}
