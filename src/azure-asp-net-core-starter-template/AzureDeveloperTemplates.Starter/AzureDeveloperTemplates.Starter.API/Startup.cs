using AzureDeveloperTemplates.Starter.API.BackgroundServices;
using AzureDeveloperTemplates.Starter.API.BackgroundServices.Channels;
using AzureDeveloperTemplates.Starter.API.Core.DependencyInjection;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureDeveloperTemplates.Starter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddAppConfiguration(Configuration)
                    .AddDataServices(Configuration)
                    .AddAzureServices()
                    .AddMessagingService();

            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<FileProcessingChannel>();
            services.AddHostedService<FileProcessingBackgroundService>();
            services.AddHealthChecks();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}
