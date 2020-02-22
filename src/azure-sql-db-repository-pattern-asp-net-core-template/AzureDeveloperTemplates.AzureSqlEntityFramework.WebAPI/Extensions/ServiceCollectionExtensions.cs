using AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Entities;
using AzureDeveloperTemplates.AzureSqlEntityFramework.Core.Interfaces;
using AzureDeveloperTemplates.AzureSqlEntityFramework.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.AzureSqlEntityFramework.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("AppDatabase")));
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IAsyncRepository<SampleEntity>), typeof(SampleRepository));
        }
    }
}
