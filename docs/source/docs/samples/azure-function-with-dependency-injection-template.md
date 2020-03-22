[Azure Function With Dependency Injection Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-function-with-dependency-injection-template)

Sample project to present how to use dependency injection in the Azure Functions.

```csharp
[assembly: FunctionsStartup(typeof(AzureDeveloperTemplates.FunctionAppWithDependencyInjection.Startup))]
namespace AzureDeveloperTemplates.FunctionAppWithDependencyInjection
{
    class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureSettings(builder);
            builder.Services.AddSingleton<IMailService, MailService>();
        }

        private void ConfigureSettings(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build();
            _configuration = config;

            var mailServiceSettings = new MailServiceSettings()
            {
                SMTPFromAddress = _configuration["MailService:SMTPFromAddress"]
            };
            builder.Services.AddSingleton(mailServiceSettings);
        }
    }
}
```