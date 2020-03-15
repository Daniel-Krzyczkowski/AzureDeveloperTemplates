using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.FunctionAppWithDependencyInjection.Services
{
    public interface IMailService
    {
        Task SendInvitation(string emailAddress);
    }
}
