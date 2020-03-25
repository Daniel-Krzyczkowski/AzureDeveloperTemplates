using AzureDeveloperTemplates.CosmosDbSdk.Core.Entities;
using AzureDeveloperTemplates.CosmosDbSdk.Core.Interfaces;
using AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data;
using AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Data.Interfaces;
using AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CosmosDbSdk.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDbSettings = new CosmosDbSettings()
            {
                Account = configuration["CosmosDb:Account"],
                Key = configuration["CosmosDb:Key"],
                DatabaseName = configuration["CosmosDb:DatabaseName"],
                ContainerName = configuration["CosmosDb:ContainerName"]
            };

            services.AddSingleton(cosmosDbSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDbClient = InitializeCosmosClientInstanceAsync(configuration.GetSection("CosmosDb"))
                                                                                             .GetAwaiter()
                                                                                             .GetResult();

            services.AddSingleton<ICosmosDbClientFactory>(cosmosDbClient);
            services.AddScoped(typeof(IAsyncRepository<SampleEntity>), typeof(SampleRepository));
        }

        private static async Task<CosmosDbClientFactory> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            CosmosClientBuilder clientBuilder = new CosmosClientBuilder(account, key);
            var serializerOptions = new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            };

            CosmosClient client = clientBuilder
                                .WithConnectionModeDirect()
                                .WithSerializerOptions(serializerOptions)
                                .Build();
            CosmosDbClientFactory cosmosDbService = new CosmosDbClientFactory(client);
            DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return cosmosDbService;
        }
    }
}
