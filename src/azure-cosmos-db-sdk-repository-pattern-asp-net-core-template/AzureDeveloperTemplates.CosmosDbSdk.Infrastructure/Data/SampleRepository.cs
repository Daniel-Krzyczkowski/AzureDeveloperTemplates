using AzureDeveloperTemplates.CosmosDbSdk.Core.Entities;
using AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data.Interfaces;
using AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Settings;

namespace AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data
{
    public class SampleRepository : CosmosDbRepository<SampleEntity>
    {
        public override string ContainerName => _cosmosDbSettings.ContainerName;

        public SampleRepository(ICosmosDbClientFactory cosmosDbClientFactory,
                                           CosmosDbSettings cosmosDbSettings)
                                                : base(cosmosDbClientFactory, cosmosDbSettings)
        {
        }
    }
}
