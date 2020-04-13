using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.Starter.Core.DomainModel
{
    public class Customer : IEntity
    {
        public Guid Id { get; set; }
    }
}
