using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.BlobStorage.Infrastructure.Settings
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
