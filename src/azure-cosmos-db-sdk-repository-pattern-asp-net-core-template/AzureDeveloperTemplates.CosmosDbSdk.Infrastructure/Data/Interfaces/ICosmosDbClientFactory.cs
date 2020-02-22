using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data.Interfaces
{
    public interface ICosmosDbClientFactory
    {
        public CosmosClient CosmosClient { get; }
    }
}
