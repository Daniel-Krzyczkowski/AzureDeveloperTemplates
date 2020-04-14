using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services.Interfaces
{
    public interface IDataService<T> where T : IEntity
    {
        Task<IEntity> Add(IEntity newEntity);
        Task<IEntity> Get(IEntity entity);
        Task<IEntity> Update(IEntity entity);
        Task Delete(IEntity entity);
    }
}
