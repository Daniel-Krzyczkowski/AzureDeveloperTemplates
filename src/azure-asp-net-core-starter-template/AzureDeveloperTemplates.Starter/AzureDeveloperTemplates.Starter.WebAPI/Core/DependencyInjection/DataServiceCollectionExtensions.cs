using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.WebAPI.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            services.TryAddSingleton<IDataService<IEntity>, CosmosDbDataService>();
            return services;
        }
    }
}
