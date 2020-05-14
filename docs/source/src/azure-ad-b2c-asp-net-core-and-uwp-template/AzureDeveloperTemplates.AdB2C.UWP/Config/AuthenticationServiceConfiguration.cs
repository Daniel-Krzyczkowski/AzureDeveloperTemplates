namespace AzureDeveloperTemplates.AdB2C.UWP.Config
{
    internal static class AuthenticationServiceConfiguration
    {
        public static string Tenant = "";
        public static string ClientId = "";
        public static string PolicySignUpSignIn = "B2C_1A_signup_signin";
        public static string BaseAuthority = "https://{tenant}.b2clogin.com/tfp/{tenant}.onmicrosoft.com/{policy}/oauth2/v2.0/authorize";
        public static string Authority = BaseAuthority.Replace("{tenant}", Tenant).Replace("{policy}", PolicySignUpSignIn);
        public static string[] ApiScopes = { $"https://{Tenant}.onmicrosoft.com/api/user_impersonation" };
    }
}
