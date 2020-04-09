using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AzureDeveloperTemplates.Starter.WebAPI.Dto
{
    public class FilesUpload
    {
        public IList<IFormFile> Files { get; set; }
    }
}
