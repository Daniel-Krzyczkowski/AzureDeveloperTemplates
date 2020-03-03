using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Settings
{
    public class FormRecognizerSettings
    {
        public string ApiEndpoint { get; set; }
        public string ApiKey { get; set; }
        public string ModelId { get; set; }
    }
}
