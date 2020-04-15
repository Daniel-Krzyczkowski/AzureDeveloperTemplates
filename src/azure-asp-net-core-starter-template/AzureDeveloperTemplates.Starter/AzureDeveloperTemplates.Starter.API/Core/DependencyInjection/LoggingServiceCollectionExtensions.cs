using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace AzureDeveloperTemplates.Starter.API.Core.DependencyInjection
{
    public static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureApplicationInsightsConfiguration = serviceProvider.GetRequiredService<IApplicationInsightsServiceConfiguration>();

            services.AddLogging(builder =>
            {
                builder.AddApplicationInsights(azureApplicationInsightsConfiguration.InstrumentationKey);
                builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Error);
            });

            services.AddApplicationInsightsTelemetry();
            return services;
        }
    }
}
