using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataService<Product> _dataService;
        public ProductService(IDataService<Product> dataServices)
        {
            _dataService = dataServices;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var allProductsResult = await _dataService.GetAllAsync();
            return allProductsResult;
        }
    }
}
