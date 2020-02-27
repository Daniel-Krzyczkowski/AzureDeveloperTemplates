using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.BlobStorage.WebAPI.Utils
{
    public class FilesUpload
    {
        public IList<IFormFile> Files { get; set; }
    }
}
