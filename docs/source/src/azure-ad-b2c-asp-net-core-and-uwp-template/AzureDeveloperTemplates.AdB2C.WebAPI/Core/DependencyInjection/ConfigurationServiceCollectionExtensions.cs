using AzureDeveloperTemplates.AdB2C.WebAPI.Core.Configuration;
using AzureDeveloperTemplates.AdB2C.WebAPI.Core.Configuration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.AdB2C.WebAPI.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AzureAdB2cServiceConfiguration>(config.GetSection("AzureAdB2C"));
            services.AddSingleton<IValidateOptions<AzureAdB2cServiceConfiguration>, AzureAdB2cServiceConfigurationValidation>();
            var azureAdB2cServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureAdB2cServiceConfiguration>>().Value;
            services.AddSingleton<IAzureAdB2cServiceConfiguration>(azureAdB2cServiceConfiguration);

            return services;
        }
    }
}
