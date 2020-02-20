using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureDeveloperTemplates.FunctionAppWithDependencyInjection.Services;

namespace AzureDeveloperTemplates.FunctionAppWithDependencyInjection
{
    public class MailTrigger
    {
        private readonly IMailService _mailService;

        public MailTrigger(IMailService mailService)
        {
            _mailService = mailService;
        }

        [FunctionName("mail-trigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string email = req.Query["email"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            email = email ?? data?.email;

            await _mailService.SendInvitation(email);

            return email != null
                ? (ActionResult)new OkObjectResult($"Email successfully sent to: {email}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
