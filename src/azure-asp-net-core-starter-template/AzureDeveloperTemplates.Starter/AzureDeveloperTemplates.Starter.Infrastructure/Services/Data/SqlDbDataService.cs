using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class SqlDbDataService : IDataService<IEntity>
    {
        public Task Add(IEntity newEntity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid entity)
        {
            throw new NotImplementedException();
        }

        public Task Get(Guid entity)
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        public Task Update(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
