using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces
{
    public interface IDataServiceFactory
    {
        public abstract IDataService<IEntity> Create();
    }
}
