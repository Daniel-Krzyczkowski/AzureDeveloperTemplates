using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces
{
    public interface IDataService<T> where T : IEntity
    {
        Task Initialize();
        Task Add(IEntity newEntity);
        Task Get(Guid entity);
        Task Update(IEntity entity);
        Task Delete(Guid entity);
    }
}
