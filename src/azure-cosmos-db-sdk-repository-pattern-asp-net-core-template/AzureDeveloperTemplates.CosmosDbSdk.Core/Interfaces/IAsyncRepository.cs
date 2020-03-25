using AzureDeveloperTemplates.CosmosDbSdk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CosmosDbSdk.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
