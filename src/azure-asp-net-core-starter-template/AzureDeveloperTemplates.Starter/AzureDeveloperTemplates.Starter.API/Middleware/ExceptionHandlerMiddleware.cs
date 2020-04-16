using AzureDeveloperTemplates.Starter.Core.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                LogException(ex);

                var statusCode = HttpStatusCode.InternalServerError;
                var result = JsonConvert.SerializeObject(new ErrorMessage("Unexpected error has occurred",
                                                "Unfortunately unexpected error has occured. Please try again later or contact system administrator"));
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync(result);
            }
        }

        private void LogException(Exception exception)
        {
            _logger.LogError("Unhandled exception was thrown:");
            _logger.LogError(exception.Message);
            _logger.LogError(exception.StackTrace);
        }
    }
}
