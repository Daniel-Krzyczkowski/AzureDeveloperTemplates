[Azure clients integration with ASP .NET Core using Microsoft.Extensions.Azure.Core Library Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-core-extensions-asp-net-core-template)

Sample project to present how to use Microsoft.Extensions.Azure.Core library to integrate Azure clients with ASP.NET Core dependency injection and configuration systems.

#### Packages used:
1. [Microsoft.Extensions.Azure](https://www.nuget.org/packages/Microsoft.Extensions.Azure/)
2. [Azure.Storage.Blobs](https://www.nuget.org/packages/Azure.Storage.Blobs/)

#### Code sample preview:

```csharp
        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SimpleTracingPolicy>();

            services.AddAzureClients(builder =>
            {
                TokenCredential credential = new DefaultAzureCredential();
#if DEBUG
                credential = new ClientSecretCredential(configuration["AZURE_TENANT_ID"],
                                                        configuration["AZURE_CLIENT_ID"],
                                                        configuration["AZURE_CLIENT_SECRET"]);
#endif

                builder.AddBlobServiceClient(new Uri(configuration["BlobStorageSettings:BlobStorageUrl"]))
                       .ConfigureOptions((options, provider) =>
                        {
                            options.Retry.MaxRetries = 10;
                            options.Retry.Delay = TimeSpan.FromSeconds(3);
                            options.Diagnostics.IsLoggingEnabled = false;
                            options.Diagnostics.ApplicationId = "AzureDeveloperTemplates";
                            options.AddPolicy(provider.GetService<SimpleTracingPolicy>(), HttpPipelinePosition.PerCall);
                        });
            });
        }
```