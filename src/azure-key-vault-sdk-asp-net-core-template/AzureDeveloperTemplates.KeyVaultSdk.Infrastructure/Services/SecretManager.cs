using Azure.Security.KeyVault.Secrets;
using AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services
{
    public class SecretManager : ISecretManager
    {
        protected readonly IKeyVaultSecretClientClientFactory _keyVaultSecretClientClientFactory;
        private readonly SecretClient _secretClient;

        public SecretManager(IKeyVaultSecretClientClientFactory keyVaultSecretClientClientFactory)
        {
            _keyVaultSecretClientClientFactory = keyVaultSecretClientClientFactory;

            _secretClient = _keyVaultSecretClientClientFactory.SecretClient;
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
            if (secret != null)
            {
                return secret.Value;
            }

            else
            {
                return string.Empty;
            }
        }

        public async Task SetSecretAsync(string secretName, string secretValue)
        {
            await _secretClient.SetSecretAsync(secretName, secretValue);
        }
    }
}
