using AzureDeveloperTemplates.Starter.Core.Services;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.WebAPI.Core.DependencyInjection
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.TryAddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
