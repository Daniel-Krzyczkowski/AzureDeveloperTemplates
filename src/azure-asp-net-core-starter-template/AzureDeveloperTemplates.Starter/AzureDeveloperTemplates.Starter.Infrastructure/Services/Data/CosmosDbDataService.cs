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
        private readonly ICosmosDbDataServiceConfiguration _dataServiceConfiguration;
        private readonly CosmosClient _client;
        public CosmosDbDataService(ICosmosDbDataServiceConfiguration dataServiceConfiguration, CosmosClient client)
        {
            _dataServiceConfiguration = dataServiceConfiguration;
            _client = client;
        }

        public async Task<IEntity> Add(IEntity newEntity)
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);
            ItemResponse<IEntity> createResponse = await container.CreateItemAsync(newEntity);
            return createResponse.Value;
        }

        public async Task Delete(IEntity entity)
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);
            await container.DeleteItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
        }

        public async Task<IEntity> Get(IEntity entity)
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);

            ItemResponse<IEntity> entityResult = await container
                                                       .ReadItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
            return entityResult.Value;
        }

        public async Task<IEntity> Update(IEntity entity)
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);

            ItemResponse<IEntity> entityResult = await container
                                                       .ReadItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));

            if (entityResult != null)
            {
                await container
                      .ReplaceItemAsync(entity, entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
            }
            return entity;
        }
    }
}
