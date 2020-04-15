using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();

            services.AddDbContext<SqlDbContext>(c =>
               c.UseSqlServer(sqlDbConfiguration.ConnectionString));

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
