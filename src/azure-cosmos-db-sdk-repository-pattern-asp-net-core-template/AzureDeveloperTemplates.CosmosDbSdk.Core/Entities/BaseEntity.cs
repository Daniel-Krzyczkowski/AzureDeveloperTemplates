using System;

namespace AzureDeveloperTemplates.CosmosDbSdk.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
