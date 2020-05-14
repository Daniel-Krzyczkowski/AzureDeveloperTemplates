using AzureDeveloperTemplates.AdB2C.UWP.Config;
using AzureDeveloperTemplates.AdB2C.UWP.Services.Contract;
using AzureDeveloperTemplates.AdB2C.UWP.Services.Interfaces;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.AdB2C.UWP.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IPublicClientApplication _publicClientApp;

        public AuthenticationService()
        {
            _publicClientApp = PublicClientApplicationBuilder.Create(AuthenticationServiceConfiguration.ClientId)
                .WithB2CAuthority(AuthenticationServiceConfiguration.Authority)
                .Build();
        }

        public async Task<AuthenticationData> Authenticate()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await _publicClientApp.GetAccountsAsync();
            try
            {
                IAccount currentUserAccount = GetAccountByPolicy(accounts, AuthenticationServiceConfiguration.PolicySignUpSignIn);
                authResult = await _publicClientApp.AcquireTokenSilent(AuthenticationServiceConfiguration.ApiScopes, currentUserAccount)
                    .ExecuteAsync();
                return new AuthenticationData
                {
                    AccessToken = authResult.AccessToken
                };

            }
            catch (MsalUiRequiredException msalUiRequiredException)
            {
                if (msalUiRequiredException.Message.Equals("No account or login hint was passed to the AcquireTokenSilent call."))
                {
                    authResult = await HandleFirstTimeAuthentication(accounts);
                    if (authResult != null)
                    {
                        return new AuthenticationData
                        {
                            AccessToken = authResult.AccessToken
                        };
                    }
                }

                else
                {
                    System.Diagnostics.Debug.WriteLine(nameof(MsalUiRequiredException) + msalUiRequiredException.Message);
                }
            }

            return null;
        }

        private async Task<AuthenticationResult> HandleFirstTimeAuthentication(IEnumerable<IAccount> accounts)
        {
            try
            {
                AuthenticationResult authResult = await _publicClientApp.AcquireTokenInteractive(AuthenticationServiceConfiguration.ApiScopes)
                     .WithAccount(GetAccountByPolicy(accounts, AuthenticationServiceConfiguration.PolicySignUpSignIn))
                     .WithPrompt(Prompt.SelectAccount)
                     .ExecuteAsync();
                return authResult;
            }
            catch (MsalException msalClientException)
            {
                if (!msalClientException.ErrorCode.Equals("authentication_canceled"))
                {
                    System.Diagnostics.Debug.WriteLine(nameof(MsalClientException) + msalClientException.Message);
                }

                return null;
            }
        }

        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }
    }
}
