using AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data
{
    public class CosmosDbClientFactory : ICosmosDbClientFactory
    {
        public CosmosDbClientFactory(CosmosClient cosmosClient)
        {
            CosmosClient = cosmosClient;
        }

        public CosmosClient CosmosClient { get; }
    }
}
