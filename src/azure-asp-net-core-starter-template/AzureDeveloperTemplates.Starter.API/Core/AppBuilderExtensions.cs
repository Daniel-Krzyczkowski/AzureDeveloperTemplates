using Microsoft.AspNetCore.Builder;

namespace AzureDeveloperTemplates.Starter.API.Core
{
    public static class AppBuilderExtensions
    {
        public static void UseSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Azure Developer Templates - Sterter API v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
