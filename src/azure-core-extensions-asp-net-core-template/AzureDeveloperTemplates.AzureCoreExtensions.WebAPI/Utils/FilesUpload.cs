using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Utils
{
    public class FilesUpload
    {
        public IList<IFormFile> Files { get; set; }
    }
}
