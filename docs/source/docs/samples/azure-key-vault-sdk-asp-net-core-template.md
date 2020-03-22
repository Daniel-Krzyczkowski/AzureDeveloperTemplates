[Azure Key Vault SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-key-vault-sdk-asp-net-core-template)

Sample project to present how to integrate with the Azure Key Vault to eliminate storing credentials in the code.

#### Packages used:
1. [Azure.Identity](https://www.nuget.org/packages/Azure.identity?WT.mc_id=ondotnet-c9-cephilli)
2. [Azure.Security.KeyVault.Secrets](https://www.nuget.org/packages/Azure.Security.KeyVault.Secrets/)

#### Code sample preview:

```csharp
        private static KeyVaultSecretClientClientFactory InitializeSecretClientInstanceAsync(IConfiguration configuration)
        {
            string keyVaultUrl = configuration["KeyVaultSettings:Url"];
            var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
            var keyVaultSecretClientClientFactory = new KeyVaultSecretClientClientFactory(secretClient);
            return keyVaultSecretClientClientFactory;
        }
```


```csharp
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

        public async Task DeleteSecret(string secretName)
        {
            DeleteSecretOperation operation = await _secretClient.StartDeleteSecretAsync(secretName);
        }

        public async Task UpdateSecret(string secretName, string secretValue)
        {
            await SetSecretAsync(secretName, secretValue);
        }
    }
```