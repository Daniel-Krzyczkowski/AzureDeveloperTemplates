using AzureDeveloperTemplates.AdB2C.WebApp.Core.Configuration;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AzureDeveloperTemplates.AdB2C.WebApp.Core.DependencyInjection
{
    public static class ConfigurationServiceCollectionExtensions
    {
        public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AzureADB2COptions>(config.GetSection("AzureAdB2C"));
            services.AddSingleton<IValidateOptions<AzureADB2COptions>, AzureADB2COptionsValidation>();
            var azureAdB2cServiceConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureADB2COptions>>().Value;
            services.AddSingleton<AzureADB2COptions>(azureAdB2cServiceConfiguration);

            return services;
        }
    }
}
