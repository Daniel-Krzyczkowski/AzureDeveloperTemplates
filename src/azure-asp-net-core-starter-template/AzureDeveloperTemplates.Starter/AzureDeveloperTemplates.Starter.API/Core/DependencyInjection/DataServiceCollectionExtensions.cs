using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped(typeof(IDataService<Product>), typeof(SqlDbDataService<Product>));
            services.AddScoped(typeof(IDataService<ProductLocation>), typeof(CosmosDbDataService<ProductLocation>));

            return services;
        }
    }
}
