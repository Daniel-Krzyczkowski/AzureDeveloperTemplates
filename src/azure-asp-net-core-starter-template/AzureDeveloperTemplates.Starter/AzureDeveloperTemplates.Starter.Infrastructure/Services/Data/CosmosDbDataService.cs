using Azure;
using Azure.Cosmos;
using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using System.Collections.Generic;
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

        public async Task<IEntity> AddAsync(IEntity newEntity)
        {
            var container = GetContainer();

            ItemResponse<IEntity> createResponse = await container.CreateItemAsync(newEntity);
            return createResponse.Value;
        }

        public async Task DeleteAsync(IEntity entity)
        {
            var container = GetContainer();

            await container.DeleteItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
        }

        public async Task<IEntity> GetAsync(IEntity entity)
        {
            var container = GetContainer();

            ItemResponse<IEntity> entityResult = await container
                                                       .ReadItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
            return entityResult.Value;
        }

        public async Task<IEntity> UpdateAsync(IEntity entity)
        {
            var container = GetContainer();

            ItemResponse<IEntity> entityResult = await container
                                                       .ReadItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));

            if (entityResult != null)
            {
                await container
                      .ReplaceItemAsync(entity, entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
            }
            return entity;
        }

        public async Task<IReadOnlyList<IEntity>> GetAllAsync()
        {
            var container = GetContainer();
            var sqlQueryText = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            AsyncPageable<IEntity> queryResultSetIterator = container.GetItemQueryIterator<IEntity>(queryDefinition);
            var iterator = queryResultSetIterator.GetAsyncEnumerator();
            List<IEntity> entities = new List<IEntity>();

            try
            {
                while (await iterator.MoveNextAsync())
                {
                    var entity = iterator.Current;
                    entities.Add(entity);
                }
            }

            finally
            {
                if (iterator != null)
                {

                    await iterator.DisposeAsync();
                }
            }

            return entities;
        }


        private CosmosContainer GetContainer()
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);
            return container;
        }
    }
}
