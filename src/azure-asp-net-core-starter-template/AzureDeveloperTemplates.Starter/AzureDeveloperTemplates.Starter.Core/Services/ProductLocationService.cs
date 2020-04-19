using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services
{
    public class ProductLocationService : IProductLocationService
    {
        private readonly IDataService<ProductLocation> _dataService;
        public ProductLocationService(IDataService<ProductLocation> dataServices)
        {
            _dataService = dataServices;
        }

        public async Task<IReadOnlyList<ProductLocation>> GetAllAsync()
        {
            var allProductsLocationsResult = await _dataService.GetAllAsync();
            return allProductsLocationsResult;
        }

        public async Task<ProductLocation> AddNewAsync(ProductLocation productLocation)
        {
            var newProductLocationResult = await _dataService.AddAsync(productLocation);
            return newProductLocationResult;
        }
    }
}
