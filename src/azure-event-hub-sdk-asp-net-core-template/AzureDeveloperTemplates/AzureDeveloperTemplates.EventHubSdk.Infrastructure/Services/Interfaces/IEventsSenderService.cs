using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.EventHubSdk.Infrastructure.Services.Interfaces
{
    public interface IEventsSenderService
    {
        Task SendEventAsync(string eventBody);
    }
}
