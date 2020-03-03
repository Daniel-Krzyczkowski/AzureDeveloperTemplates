using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.CognitiveSearch.CustomSkills.Infrastructure.Model
{
    public class WebApiSkillRequest
    {
        public List<WebApiRequestRecord> Values { get; set; } = new List<WebApiRequestRecord>();
    }
}
