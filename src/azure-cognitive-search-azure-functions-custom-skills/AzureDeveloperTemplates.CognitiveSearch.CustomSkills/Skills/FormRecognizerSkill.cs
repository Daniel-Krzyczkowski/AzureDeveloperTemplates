using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Constants;
using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Model;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills
{
    public class FormRecognizerSkill
    {
        private readonly IDocumentProcessingService _documentProcessingService;

        public FormRecognizerSkill(IDocumentProcessingService documentProcessingService)
        {
            _documentProcessingService = documentProcessingService;
        }

        [FunctionName(ServiceConstants.FormAnalyzerServiceName)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{ServiceConstants.FormAnalyzerServiceName} Custom Skill: C# HTTP trigger function processed a request.");

            IList<WebApiRequestRecord> requestRecords = await _documentProcessingService.DeserializeRequest(req);
            if (requestRecords == null)
            {
                return new BadRequestObjectResult($"{ServiceConstants.FormAnalyzerServiceName} - Invalid request record array.");
            }

            WebApiSkillResponse response = await _documentProcessingService.ProcessRequestRecordsAsync(requestRecords);
            return new OkObjectResult(response);
        }
    }
}
