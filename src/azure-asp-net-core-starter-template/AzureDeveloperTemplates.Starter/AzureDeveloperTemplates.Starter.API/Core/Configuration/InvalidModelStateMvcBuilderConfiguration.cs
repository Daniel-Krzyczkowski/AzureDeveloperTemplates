using AzureDeveloperTemplates.Starter.Core.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace AzureDeveloperTemplates.Starter.API.Core.Configuration
{
    public static class InvalidModelStateMvcBuilderConfiguration
    {
        public static void ConfigureInvalidModelStateHandling(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errors = string.Join(' ', c.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage));

                    return new BadRequestObjectResult(new ErrorMessage("Model validation failed", errors));
                };
            });
        }
    }
}
