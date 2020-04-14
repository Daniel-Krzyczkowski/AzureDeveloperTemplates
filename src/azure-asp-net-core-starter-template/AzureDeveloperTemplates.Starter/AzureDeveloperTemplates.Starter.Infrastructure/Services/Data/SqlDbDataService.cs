using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class SqlDbDataService : IDataService<IEntity>
    {
        private SqlDbContext _sqlDbContext;

        public SqlDbDataService(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        public async Task<IEntity> Add(IEntity newEntity)
        {
            var entityResult = _sqlDbContext.Add(newEntity);
            await _sqlDbContext.SaveChangesAsync();
            return entityResult.Entity;
        }

        public async Task Delete(IEntity entity)
        {
            var entityResult = await _sqlDbContext.Set<IEntity>()
                   .Where(e => e.Id == entity.Id)
                   .FirstOrDefaultAsync();
            if (entityResult != null)
            {
                _sqlDbContext.Set<IEntity>().Remove(entity);
                await _sqlDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEntity> Get(IEntity entity)
        {
            var entityResult = await _sqlDbContext.Set<IEntity>()
                                .Where(e => e.Id == entity.Id)
                                .FirstOrDefaultAsync();
            return entityResult;
        }

        public async Task<IEntity> Update(IEntity entity)
        {
            var entityResult = await _sqlDbContext.Set<IEntity>()
                               .Where(e => e.Id == entity.Id)
                               .FirstOrDefaultAsync();

            if (entityResult.Id == entity.Id)
            {
                _sqlDbContext.Set<IEntity>().Update(entity);
                await _sqlDbContext.SaveChangesAsync();
            }
            return entity;
        }
    }
}
