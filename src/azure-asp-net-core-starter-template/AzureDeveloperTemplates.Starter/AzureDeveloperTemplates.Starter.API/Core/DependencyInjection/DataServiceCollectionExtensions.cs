using Azure.Cosmos;
using Azure.Cosmos.Serialization;
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

            var cosmoDbConfiguration = serviceProvider.GetRequiredService<ICosmosDbDataServiceConfiguration>();
            CosmosClientOptions cosmosClientOptions = new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            };
            CosmosClient cosmosClient = new CosmosClient(cosmoDbConfiguration.ConnectionString, cosmosClientOptions);
            CosmosDatabase database = cosmosClient.CreateDatabaseIfNotExistsAsync(cosmoDbConfiguration.DatabaseName)
                                                   .GetAwaiter()
                                                   .GetResult();
            CosmosContainer container = database.CreateContainerIfNotExistsAsync(
                cosmoDbConfiguration.ContainerName,
                cosmoDbConfiguration.PartitionKeyPath,
                400)
                .GetAwaiter()
                .GetResult();

            services.AddSingleton(cosmosClient);


            var sqlDbConfiguration = serviceProvider.GetRequiredService<ISqlDbDataServiceConfiguration>();

            services.AddDbContext<SqlDbContext>(c =>
               c.UseSqlServer(sqlDbConfiguration.ConnectionString));
            services.AddScoped(typeof(IDataService<Product>), typeof(SqlDbDataService<Product>));
            services.AddScoped(typeof(IDataService<ProductLocation>), typeof(CosmosDbDataService<ProductLocation>));

            return services;
        }
    }
}
