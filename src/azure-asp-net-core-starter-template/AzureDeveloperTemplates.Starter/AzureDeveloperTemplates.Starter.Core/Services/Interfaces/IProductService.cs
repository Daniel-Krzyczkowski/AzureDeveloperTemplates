using AzureDeveloperTemplates.Starter.Core.DomainModel;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task AddToBasket(Product product);
    }
}
