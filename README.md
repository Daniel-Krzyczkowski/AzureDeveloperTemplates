![AzureDeveloperTemplates.png](images/AzureDeveloperTemplates.png)

# Introduction
## This project and repository was created to collect templates related with Azure Services .NET SDK integration and commonly used design patterns.

### Below there is a list of repositories presented together with descriptions:

#### [1. Azure Function With Dependency Injection Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-function-with-dependency-injection-template/AzureDeveloperTemplates)

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

#### [2. Azure Application Insights SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-application-insights-sdk-asp-net-core-template)

Aaa

#### 3. [Azure Cosmos DB SDK Repository Pattern with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-cosmos-db-sdk-repository-pattern-asp-net-core-template)

Aaa

#### 4. [Azure Key Vault SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-key-vault-sdk-asp-net-core-template)

Aaa

#### 5. [Azure Notification Hub SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-notification-hub-sdk-asp-net-core-template)

Aaa

#### 6. [Azure SignalR Service SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-signalr-service-sdk-asp-net-core-template)

Aaa

#### [7. Azure SQL DB Repository Pattern with Entity Framework Core and ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-sql-db-repository-pattern-asp-net-core-template)

Aaa

#### 8. Azure Service Bus SDK with ASP .NET Core Template

TBA

#### 9. Azure Cognitive Search SDK with ASP .NET Core Template

TBA
