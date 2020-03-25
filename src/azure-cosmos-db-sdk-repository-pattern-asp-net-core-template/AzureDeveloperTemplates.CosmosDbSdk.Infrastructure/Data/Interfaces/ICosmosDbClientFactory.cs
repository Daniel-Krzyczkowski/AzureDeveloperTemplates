using Microsoft.Azure.Cosmos;

namespace AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data.Interfaces
{
    public interface ICosmosDbClientFactory
    {
        public CosmosClient CosmosClient { get; }
    }
}
