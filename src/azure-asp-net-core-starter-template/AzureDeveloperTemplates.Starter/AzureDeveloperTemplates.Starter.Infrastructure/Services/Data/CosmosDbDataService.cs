using Azure.Cosmos;
using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class CosmosDbDataService : IDataService<IEntity>
    {
        private ICosmosDbDataServiceConfiguration _dataServiceConfiguration;
        private CosmosClient _client;
        public CosmosDbDataService(ICosmosDbDataServiceConfiguration dataServiceConfiguration, CosmosClient client)
        {
            _dataServiceConfiguration = dataServiceConfiguration;
            _client = client;
        }


        public async Task Initialize()
        {
            CosmosDatabase database = await _client.CreateDatabaseIfNotExistsAsync(_dataServiceConfiguration.DatabaseName);
            CosmosContainer container = await database.CreateContainerIfNotExistsAsync(
                _dataServiceConfiguration.ContainerName,
                _dataServiceConfiguration.PartitionKeyPath,
                400);
        }

        public Task Add(IEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid entity)
        {
            throw new NotImplementedException();
        }

        public Task Get(Guid entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
