using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Services.Interfaces
{
    public interface IDocumentContentExtractor
    {
        Task<byte[]> DownloadDocument(string documentUrl);
    }
}
