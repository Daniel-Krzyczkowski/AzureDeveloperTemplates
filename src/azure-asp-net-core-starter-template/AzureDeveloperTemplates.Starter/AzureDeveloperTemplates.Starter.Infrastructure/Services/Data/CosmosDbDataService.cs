using Azure;
using Azure.Cosmos;
using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class CosmosDbDataService : IDataService<IEntity>
    {
        private readonly ICosmosDbDataServiceConfiguration _dataServiceConfiguration;
        private readonly CosmosClient _client;
        private readonly ILogger<CosmosDbDataService> _logger;

        public CosmosDbDataService(ICosmosDbDataServiceConfiguration dataServiceConfiguration, CosmosClient client,
                                                                                               ILogger<CosmosDbDataService> logger)
        {
            _dataServiceConfiguration = dataServiceConfiguration;
            _client = client;
            _logger = logger;
        }

        public async Task<IEntity> AddAsync(IEntity newEntity)
        {
            try
            {
                var container = GetContainer();

                ItemResponse<IEntity> createResponse = await container.CreateItemAsync(newEntity);
                return createResponse.Value;
            }
            catch (CosmosException ex)
            {
                _logger.LogError($"New entity with ID: {newEntity.Id} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(IEntity entity)
        {
            try
            {
                var container = GetContainer();

                await container.DeleteItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
            }
            catch (CosmosException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not removed successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IEntity> GetAsync(IEntity entity)
        {
            try
            {
                var container = GetContainer();

                ItemResponse<IEntity> entityResult = await container
                                                           .ReadItemAsync<IEntity>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
                return entityResult.Value;
            }
            catch (CosmosException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not retrieved successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IEntity> UpdateAsync(IEntity entity)
        {
            try
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
            catch (CosmosException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not updated successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IReadOnlyList<IEntity>> GetAllAsync()
        {
            try
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
            catch (CosmosException ex)
            {
                _logger.LogError($"Entities was not retrieved successfully - error details: {ex.Message}");
                throw;
            }
        }


        private CosmosContainer GetContainer()
        {
            var database = _client.GetDatabase(_dataServiceConfiguration.DatabaseName);
            var container = database.GetContainer(_dataServiceConfiguration.ContainerName);
            return container;
        }
    }
}
