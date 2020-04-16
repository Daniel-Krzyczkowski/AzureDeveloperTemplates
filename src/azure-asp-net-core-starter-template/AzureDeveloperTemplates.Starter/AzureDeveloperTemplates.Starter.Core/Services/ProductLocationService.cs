using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using System;
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

        public Task<ProductLocation> AddNewAsync(ProductLocation product)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ProductLocation>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
