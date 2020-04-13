using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.WebAPI.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SqlDbContext>(c =>
               c.UseSqlServer(config.GetConnectionString("SqlDbConnectionString")));

            //Order of services registration is crucial - they will be retrievied in the specific order in the controllers:
            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Singleton<IDataService<IEntity>, CosmosDbDataService>(),
                ServiceDescriptor.Scoped<IDataService<IEntity>, SqlDbDataService>()
            });
            return services;
        }
    }
}
