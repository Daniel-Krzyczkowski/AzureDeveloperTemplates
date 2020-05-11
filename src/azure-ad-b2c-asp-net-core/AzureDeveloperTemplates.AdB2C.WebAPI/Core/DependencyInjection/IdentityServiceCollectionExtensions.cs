using AzureDeveloperTemplates.AdB2C.WebAPI.Core.Configuration.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace AzureDeveloperTemplates.AdB2C.WebAPI.Core.DependencyInjection
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var azureAdB2cConfiguration = serviceProvider.GetRequiredService<IAzureAdB2cServiceConfiguration>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(jwtOptions =>
                   {
                       jwtOptions.Authority = $"https://{azureAdB2cConfiguration.Tenant}.b2clogin.com/tfp/{azureAdB2cConfiguration.Tenant}.onmicrosoft.com/{azureAdB2cConfiguration.Policy}/v2.0/";
                       jwtOptions.Audience = azureAdB2cConfiguration.ClientId;
                   });

            return services;
        }
    }
}
