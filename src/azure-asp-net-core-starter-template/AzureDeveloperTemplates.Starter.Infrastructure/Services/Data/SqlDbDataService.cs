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
    public sealed class SqlDbDataService<T> : IDataService<T> where T : class, IEntity
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly ILogger<SqlDbDataService<T>> _logger;

        public SqlDbDataService(SqlDbContext sqlDbContext, ILogger<SqlDbDataService<T>> logger)
        {
            _sqlDbContext = sqlDbContext;
            _logger = logger;
        }

        public async Task<T> AddAsync(T newEntity)
        {
            try
            {
                newEntity.Id = Guid.NewGuid();
                var entityResult = _sqlDbContext.Set<T>().Add(newEntity);
                await _sqlDbContext.SaveChangesAsync();
                return entityResult.Entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"New entity with ID: {newEntity.Id} was not added successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                var entityResult = await _sqlDbContext.Set<T>()
                                                       .Where(e => e.Id == entity.Id)
                                                       .FirstOrDefaultAsync();
                if (entityResult != null)
                {
                    _sqlDbContext.Set<T>().Remove(entity);
                    await _sqlDbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Entity with ID: {entity.Id} was not removed successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<T> GetAsync(T entity)
        {
            try
            {
                var entityResult = await _sqlDbContext.Set<T>()
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

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var entityResult = await _sqlDbContext.Set<T>()
                   .Where(e => e.Id == entity.Id)
                   .FirstOrDefaultAsync();

                if (entityResult.Id == entity.Id)
                {
                    _sqlDbContext.Set<T>().Update(entity);
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

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                var allProducts = await _sqlDbContext.Set<T>()
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
