using AzureDeveloperTemplates.Starter.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<Product> AddNewAsync(Product product);
    }
}
