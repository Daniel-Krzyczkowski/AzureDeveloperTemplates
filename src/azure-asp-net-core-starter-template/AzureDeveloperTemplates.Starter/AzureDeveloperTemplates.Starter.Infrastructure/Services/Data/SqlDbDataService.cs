using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Data
{
    public class SqlDbDataService : IDataService<IEntity>
    {
        private SqlDbContext _sqlDbContext;
        private readonly ILogger<SqlDbDataService> _logger;

        public SqlDbDataService(SqlDbContext sqlDbContext, ILogger<SqlDbDataService> logger)
        {
            _sqlDbContext = sqlDbContext;
            _logger = logger;
        }

        public async Task<IEntity> AddAsync(IEntity newEntity)
        {
            try
            {
                newEntity.Id = Guid.NewGuid();
                var entityResult = _sqlDbContext.Add(newEntity);
                await _sqlDbContext.SaveChangesAsync();
                return entityResult.Entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"New entity with ID: {newEntity.Id} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(IEntity entity)
        {
            try
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
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not removed successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IEntity> GetAsync(IEntity entity)
        {
            try
            {
                var entityResult = await _sqlDbContext.Set<IEntity>()
                    .Where(e => e.Id == entity.Id)
                    .FirstOrDefaultAsync();
                return entityResult;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not retrieved successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IEntity> UpdateAsync(IEntity entity)
        {
            try
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
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not updated successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<IReadOnlyList<IEntity>> GetAllAsync()
        {
            try
            {
                var allProducts = await _sqlDbContext.Set<IEntity>()
                                     .ToListAsync();
                return allProducts;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Entities was not retrieved successfully - error details: {ex.Message}");
                throw;
            }
        }
    }
}
