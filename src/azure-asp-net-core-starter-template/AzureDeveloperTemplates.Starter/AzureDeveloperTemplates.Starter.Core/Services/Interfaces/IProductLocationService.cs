using AzureDeveloperTemplates.Starter.Core.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services.Interfaces
{
    public interface IProductLocationService
    {
        Task<IReadOnlyList<ProductLocation>> GetAllAsync();
        Task<ProductLocation> AddNewAsync(ProductLocation product);
    }
}
