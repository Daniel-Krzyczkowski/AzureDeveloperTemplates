using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using System;

namespace AzureDeveloperTemplates.Starter.Core.DomainModel
{
    public class ProductLocation : IEntity
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Guid ProductId { get; set; }
    }
}
