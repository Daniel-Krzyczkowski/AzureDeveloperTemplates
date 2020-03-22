[Azure Application Insights SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-application-insights-sdk-asp-net-core-template)

Sample project to present how to enable logging with the Azure Application Insights.

#### Packages used:
1. [Microsoft.ApplicationInsights.AspNetCore](https://www.nuget.org/packages/Microsoft.ApplicationInsights.AspNetCore)

#### Code sample preview:

```csharp
   public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddApplicationInsightsTelemetry();
    }
```

