using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Model;
using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services
{
    public class FormRecognizerService : IFormRecognizerService
    {
        private readonly FormRecognizerSettings _formRecognizerSettings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<FormRecognizerService> _log;

        public FormRecognizerService(FormRecognizerSettings formRecognizerSettings, HttpClient httpClient,
                                                                                                    ILogger<FormRecognizerService> log)
        {
            _formRecognizerSettings = formRecognizerSettings;
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<string> AnalyzeForm(byte[] formDocument, string formDocumentUrl)
        {
            string formAnalysisUrl = _formRecognizerSettings.ApiEndpoint + "/models/" + Uri.EscapeDataString(_formRecognizerSettings.ModelId) + "/analyze";

            if (formDocument != null)
            {
                var content = new ByteArrayContent(formDocument);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                content.Headers.Add("Ocp-Apim-Subscription-Key", _formRecognizerSettings.ApiKey);

                var response = await _httpClient.PostAsync(formAnalysisUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    _log.LogError($"An error occurred when processing document form with URL: {formDocumentUrl} with Form Recognizer. Error body: {responseBody}");
                    return null;
                }

                else
                {
                    var analysisResultEndpoint = response.Headers
                                                            .GetValues("Operation-Location")
                                                            .FirstOrDefault();
                    return analysisResultEndpoint;
                }
            }

            _log.LogError($"An error occurred when processing document form with URL: {formDocumentUrl} - file is empty");
            return null;
        }

        public async Task<FormAnalysisResponse> GetFormAnalysisResult(string formAnalysisResultEndpoint)
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Ocp-Apim-Subscription-Key"))
            {
                _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _formRecognizerSettings.ApiKey);
            }

            var response = await _httpClient.GetAsync(formAnalysisResultEndpoint);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _log.LogError($"An error occurred when getting form analysis result with URL: {formAnalysisResultEndpoint}. Error body: {responseBody}");
                return null;
            }

            else
            {
                var formAnalysisResponse = JsonConvert.DeserializeObject<FormAnalysisResponse>(responseBody);
                if (formAnalysisResponse.status.Equals("notStarted"))
                {
                    Task delay = Task.Delay(5000);
                    await delay;

                    formAnalysisResponse = await GetFormAnalysisResult(formAnalysisResultEndpoint);
                    return formAnalysisResponse;
                }

                else
                {
                    return formAnalysisResponse;
                }
            }
        }
    }
}
