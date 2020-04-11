using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class CosmosDbDataServiceFactory : IDataServiceFactory
    {
        private readonly IDataService<IEntity> _cosmosDbDataService;

        public CosmosDbDataServiceFactory(IDataService<IEntity> cosmosDbDataService)
        {
            _cosmosDbDataService = cosmosDbDataService;
        }

        public IDataService<IEntity> Create()
        {
            return _cosmosDbDataService;
        }
    }
}
