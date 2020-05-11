using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.AdB2C.WebApp.Pages
{
    [Authorize]
    public class PrivateContentModel : PageModel
    {
        private AzureADB2COptions _azureADB2COptions;
        private IConfidentialClientApplication _cca;

        public PrivateContentModel(AzureADB2COptions azureADB2COptions)
        {
            _azureADB2COptions = azureADB2COptions;
        }

        public Guid UserId { get; set; }
        public async Task OnGet()
        {
            UserId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var scope = "https://cleanarchdev.onmicrosoft.com/api/user_impersonation";
            string baseAuthority = "https://cleanarchdev.b2clogin.com/tfp/{tenant}/{policy}/oauth2/v2.0/authorize";
            string authority = baseAuthority.Replace("{tenant}", _azureADB2COptions.Domain).Replace("{policy}", _azureADB2COptions.SignUpSignInPolicyId);

            try
            {
                _cca =
                  ConfidentialClientApplicationBuilder.Create(_azureADB2COptions.ClientId)
                      .WithRedirectUri("https://localhost:5001")
                      .WithClientSecret(_azureADB2COptions.ClientSecret)
                      .WithB2CAuthority(authority)
                      .Build();

                var accounts = await _cca.GetAccountsAsync();
                AuthenticationResult authResult = await _cca.AcquireTokenForClient(new List<string> { scope }).ExecuteAsync();

                var token = authResult.AccessToken;
            }
            catch (MsalUiRequiredException msalUiRequiredException)
            {
                System.Diagnostics.Debug.WriteLine(nameof(MsalUiRequiredException) + msalUiRequiredException.Message);
            }
        }
    }
}