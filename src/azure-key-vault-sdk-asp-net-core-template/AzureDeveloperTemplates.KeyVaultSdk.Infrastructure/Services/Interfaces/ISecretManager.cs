using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces
{
    public interface ISecretManager
    {
        Task<string> GetSecretAsync(string secretName);
        Task SetSecretAsync(string secretName, string secretValue);
        Task DeleteSecret(string secretName);
        Task UpdateSecret(string secretName, string secretValue);
    }
}
