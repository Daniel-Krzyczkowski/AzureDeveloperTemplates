using System;

namespace AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
