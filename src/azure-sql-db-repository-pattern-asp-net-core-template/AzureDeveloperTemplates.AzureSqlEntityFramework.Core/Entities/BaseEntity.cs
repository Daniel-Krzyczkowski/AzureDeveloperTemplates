using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
