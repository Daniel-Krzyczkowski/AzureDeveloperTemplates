using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces
{
    public interface IKeyVaultSecretClientClientFactory
    {
        SecretClient SecretClient { get; }
    }
}
