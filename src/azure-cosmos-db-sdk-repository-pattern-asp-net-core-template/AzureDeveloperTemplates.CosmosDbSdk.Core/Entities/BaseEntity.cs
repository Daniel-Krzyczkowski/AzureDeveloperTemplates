using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CosmosDbSdk.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
