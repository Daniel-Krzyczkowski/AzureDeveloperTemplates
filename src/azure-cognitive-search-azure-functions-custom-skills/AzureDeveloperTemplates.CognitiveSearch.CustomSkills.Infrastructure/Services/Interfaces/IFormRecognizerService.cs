using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services.Interfaces
{
    public interface IFormRecognizerService
    {
        Task<string> AnalyzeForm(byte[] formDocument, string formDocumentUrl);
        Task<FormAnalysisResponse> GetFormAnalysisResult(string formAnalysisResultEndpoint);
    }
}
