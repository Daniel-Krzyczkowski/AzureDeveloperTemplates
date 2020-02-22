using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CosmosDbSdk.Infrastructure.Settings
{
    public class CosmosDbSettings
    {
        public string Account { get; set; }
        public string Key { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
    }
}
