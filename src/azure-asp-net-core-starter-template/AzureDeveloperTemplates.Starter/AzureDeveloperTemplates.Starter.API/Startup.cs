using AzureDeveloperTemplates.Starter.API.BackgroundServices;
using AzureDeveloperTemplates.Starter.API.BackgroundServices.Channels;
using AzureDeveloperTemplates.Starter.API.Core;
using AzureDeveloperTemplates.Starter.API.Core.Configuration;
using AzureDeveloperTemplates.Starter.API.Core.DependencyInjection;
using AzureDeveloperTemplates.Starter.API.Core.Middleware;
using AzureDeveloperTemplates.Starter.API.Hubs;
using AzureDeveloperTemplates.Starter.Core.Services;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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
            services.Configure<HostOptions>(option =>
            {
                option.ShutdownTimeout = TimeSpan.FromSeconds(10);
            });

            services.AddAppConfiguration(Configuration)
                    .AddLoggingServices()
                    .AddIdentityService()
                    .AddDataServices()
                    .AddStorageServices()
                    .AddMessagingService()
                    .AddEventsService()
                    .AddSwagger();

            services.AddSingleton<IStorageService, StorageService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IProductLocationService, ProductLocationService>();
            services.AddSingleton<FileProcessingChannel>();
            services.AddHostedService<FileProcessingBackgroundService>();
            services.AddHealthChecks();
            services.AddControllers()
                    .ConfigureInvalidModelStateHandling();

            services.AddSignalR().AddAzureSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerServices();

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
                endpoints.MapHub<RealTimeMessageHub>("/real-time-messages");
            });
        }
    }
}
