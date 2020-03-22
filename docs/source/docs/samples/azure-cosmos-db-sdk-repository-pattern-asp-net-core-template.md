 [Azure Cosmos DB SDK Repository Pattern with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-cosmos-db-sdk-repository-pattern-asp-net-core-template)

 Sample project to present how to use repository pattern with Azure Cosmos DB.

```csharp
    public abstract class CosmosDbRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected Container _container;
        protected readonly ICosmosDbClientFactory _cosmosDbClientFactory;
        protected readonly CosmosDbSettings _cosmosDbSettings;

        public abstract string ContainerName { get; }

        public CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory,
                                  CosmosDbSettings cosmosDbSettings)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;
            _cosmosDbSettings = cosmosDbSettings;

            _container = _cosmosDbClientFactory.CosmosClient
                                               .GetContainer(_cosmosDbSettings.DatabaseName,
                                                            ContainerName);
        }

        public async Task<T> AddAsync(T entity)
        {
            var response = await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));
            return response.Resource;
        }

        public async Task DeleteAsync(T entity)
        {
            await _container.DeleteItemAsync<T>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition("Select * from c"));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(T entity)
        {
            await _container.UpsertItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));
        }
    }
```