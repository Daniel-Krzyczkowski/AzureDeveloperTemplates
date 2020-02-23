using AzureDeveloperTemplates.ServiceBusSdk.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.ServiceBusSdk.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusSettings = new ServiceBusSettings()
            {
                ConnectionString = configuration["ServiceBus:Account"],
                Topic = configuration["ServiceBus:Key"]
            };

            services.AddSingleton(serviceBusSettings);
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
