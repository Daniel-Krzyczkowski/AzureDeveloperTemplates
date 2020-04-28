using System;

namespace AzureDeveloperTemplates.Starter.Core.DomainModel.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
