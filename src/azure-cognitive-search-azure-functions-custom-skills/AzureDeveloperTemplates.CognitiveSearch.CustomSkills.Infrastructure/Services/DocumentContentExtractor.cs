using AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services
{
    public class DocumentContentExtractor : IDocumentContentExtractor
    {
        public async Task<byte[]> DownloadDocument(string documentUrl)
        {
            using (var webClient = new WebClient())
            {
                byte[] documentBytes = await webClient.DownloadDataTaskAsync(new Uri(documentUrl));
                return documentBytes;
            }
        }
    }
}
