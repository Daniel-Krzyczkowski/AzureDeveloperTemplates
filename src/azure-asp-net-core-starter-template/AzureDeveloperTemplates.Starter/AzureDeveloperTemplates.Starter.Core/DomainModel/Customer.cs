using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using System;

namespace AzureDeveloperTemplates.Starter.Core.DomainModel
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
    }
}
