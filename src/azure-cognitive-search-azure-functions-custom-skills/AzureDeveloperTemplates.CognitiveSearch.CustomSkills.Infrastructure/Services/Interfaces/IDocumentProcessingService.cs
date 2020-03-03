using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services.Interfaces
{
    public interface IDocumentProcessingService
    {
        Task<IList<WebApiRequestRecord>> DeserializeRequest(HttpRequest request);
        Task<WebApiSkillResponse> ProcessRequestRecordsAsync(IEnumerable<WebApiRequestRecord> requestRecords);
    }
}
