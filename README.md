![AzureDeveloperTemplates.png](images/AzureDeveloperTemplates.png)

# Introduction
## This project and repository was created to collect templates related with Azure Services .NET SDK integration and commonly used design patterns.

*If you like this content, please give it a star!*
![github-start.png](images/github-start2.png)

[Azure SDK Latest Releases](https://azure.github.io/azure-sdk/releases/latest/dotnet.html)


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

Sample project to present how to enable logging with the Azure Application Insights.


#### [3. Azure Cosmos DB SDK Repository Pattern with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-cosmos-db-sdk-repository-pattern-asp-net-core-template)

Sample project to present how to use repository pattern with Azure Cosmos DB.

```csharp
    public abstract class CosmosDbRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected Container _container;
        protected readonly ICosmosDbClientFactory _cosmosDbClientFactory;
        protected readonly CosmosDbSettings _cosmosDbSettings;

        public abstract string ContainerName { get; }

        public CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory,
                                  CosmosDbSettings cosmosDbSettings)
        {
            _cosmosDbClientFactory = cosmosDbClientFactory;
            _cosmosDbSettings = cosmosDbSettings;

            _container = _cosmosDbClientFactory.CosmosClient
                                               .GetContainer(_cosmosDbSettings.DatabaseName,
                                                            ContainerName);
        }

        public async Task<T> AddAsync(T entity)
        {
            var response = await _container.CreateItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));
            return response.Resource;
        }

        public async Task DeleteAsync(T entity)
        {
            await _container.DeleteItemAsync<T>(entity.Id.ToString(), new PartitionKey(entity.Id.ToString()));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition("Select * from c"));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(T entity)
        {
            await _container.UpsertItemAsync<T>(entity, new PartitionKey(entity.Id.ToString()));
        }
    }
```


#### [4. Azure Key Vault SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-key-vault-sdk-asp-net-core-template)

Sample project to present how to integrate with the Azure Key Vault to eliminate storing credentials in the code.

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
    }
```


#### 5. [Azure Notification Hub SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-notification-hub-sdk-asp-net-core-template)

Sample project to present how to use Azure Notification Hub SDK to send push notifications.

```csharp
    public class PushNotificationService : IPushNotificationService
    {
        private readonly INotificationHubFactory _notificationHubFactory;
        public PushNotificationService(INotificationHubFactory notificationHubFactory)
        {
            _notificationHubFactory = notificationHubFactory;
        }

        public async Task<string> CreateRegistrationId(string handle)
        {
            var hub = _notificationHubFactory.NotificationHubClient;
            string newRegistrationId = null;

            if (handle != null)
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(handle, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
            {
                newRegistrationId = await hub.CreateRegistrationIdAsync();
            }

            return newRegistrationId;
        }

        public async Task DeleteRegistration(string registrationId)
        {
            await _notificationHubFactory.NotificationHubClient.DeleteRegistrationAsync(registrationId);
        }

        public async Task RegisterForPushNotifications(string registrationId, DeviceRegistration deviceUpdate)
        {
            var hub = _notificationHubFactory.NotificationHubClient;
            RegistrationDescription registrationDescription = null;

            switch (deviceUpdate.Platform)
            {
                case MobilePlatform.wns:
                    registrationDescription = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case MobilePlatform.apns:
                    registrationDescription = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case MobilePlatform.fcm:
                    registrationDescription = new FcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    throw new ArgumentException("Please provide correct platform notification service name");
            }

            registrationDescription.RegistrationId = registrationId;
            if (deviceUpdate.Tags != null)
                registrationDescription.Tags = new HashSet<string>(deviceUpdate.Tags);

            try
            {
                await hub.CreateOrUpdateRegistrationAsync(registrationDescription);
            }
            catch (MessagingException exception)
            {
                System.Diagnostics.Debug.WriteLine("Unhandled exception was thrown during registration in the Azure Notification Hub:");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
            }
        }

        public async Task<NotificationOutcome> SendNotification(PushNotification newNotification)
        {
            var hub = _notificationHubFactory.NotificationHubClient;

            try
            {
                NotificationOutcome outcome = null;

                switch (newNotification.MobilePlatform)
                {
                    case MobilePlatform.wns:
                        var toast = @"<toast><visual><binding template=""ToastText01""><text id=""1"">"
                         + newNotification.Message + "</text></binding></visual></toast>";

                        if (newNotification.Tags == null)
                            outcome = await hub.SendWindowsNativeNotificationAsync(toast);
                        else
                            outcome = await hub.SendWindowsNativeNotificationAsync(toast, newNotification.Tags);
                        break;
                    case MobilePlatform.apns:
                        var alert = "{\"aps\":{\"alert\":\"" + newNotification.Message + "\"}}";

                        if (newNotification.Tags == null)
                            outcome = await hub.SendAppleNativeNotificationAsync(alert);
                        else
                            outcome = await hub.SendAppleNativeNotificationAsync(alert, newNotification.Tags);
                        break;
                    case MobilePlatform.fcm:
                        var notification = "{ \"data\" : {\"message\":\"" + newNotification.Message + "\"}}";

                        if (newNotification.Tags == null)
                            outcome = await hub.SendFcmNativeNotificationAsync(notification);
                        else
                            outcome = await hub.SendFcmNativeNotificationAsync(notification, newNotification.Tags);
                        break;
                }

                if (outcome != null)
                {
                    if (!((outcome.State == NotificationOutcomeState.Abandoned) ||
                        (outcome.State == NotificationOutcomeState.Unknown)))
                    {
                        return outcome;
                    }
                }

                System.Diagnostics.Debug.WriteLine("Notification was not sent due to issue. Please send again.");
                return null;
            }

            catch (MessagingException exception)
            {
                System.Diagnostics.Debug.WriteLine("Unhandled exception was thrown during sending notification with the Azure Notification Hub:");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                return null;
            }

            catch (ArgumentException exception)
            {
                System.Diagnostics.Debug.WriteLine("Unhandled exception was thrown during sending notification with the Azure Notification Hub:");
                System.Diagnostics.Debug.WriteLine(exception.Message);
                System.Diagnostics.Debug.WriteLine(exception.StackTrace);
                return null;
            }
        }
    }
```


#### [6. Azure SignalR Service SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-signalr-service-sdk-asp-net-core-template)

Sample project to present how to use SignalR Service to send real time messages.

```csharp
    [Authorize]
    public class SampleHub : Hub
    {
        [HubMethodName("SendDirectMessageToUser")]
        public async Task SendDirectMessageToUser(string sampleMessageAsJson)
        {
            var sampleMessage = JsonConvert.DeserializeObject<SampleMessage>(sampleMessageAsJson);

            sampleMessage.SenderId = new Guid(Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var messageAsJson = JsonConvert.SerializeObject(sampleMessage);

            await Clients.User(sampleMessage.ReceiverId.ToString()).SendAsync(messageAsJson);
        }
    }
```


#### [7. Azure SQL DB Repository Pattern with Entity Framework Core and ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-sql-db-repository-pattern-asp-net-core-template)

Sample project to present how to use repository pattern with Azure SQL DB.

```csharp
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                                                                        : base(options)
        {
        }

        public DbSet<SampleEntity> SampleEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SampleEntity>().HasData(new SampleEntity
            {
                Id = Guid.NewGuid()
            });
        }
    }
```


#### [8. Azure Service Bus SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-service-bus-sdk-asp-net-core-template)

Sample project to present how to use Azure Service Bus SDK to send and receive messages.
Many thanks to [@HaraczPawel](https://twitter.com/HaraczPawel) who helped create this sample basing on the sample from his original [repository](https://github.com/PawelHaracz/pawelharacz.com)!

```csharp
    public class MessagesReceiverService : IMessagesReceiverService
    {
        private readonly ServiceBusSettings _serviceBusSettings;
        private readonly IMessageReceiver _messageReceiver;

        public MessagesReceiverService(ServiceBusSettings serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings;

            var connectionString = new ServiceBusConnectionStringBuilder(_serviceBusSettings.ConnectionString);
            _messageReceiver = new MessageReceiver(connectionString.GetNamespaceConnectionString(),
                EntityNameHelper.FormatSubscriptionPath(_serviceBusSettings.TopicName, _serviceBusSettings.Subscription),
                ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);
        }

        public Task<Message> ReceiveMessageAsync() => _messageReceiver.ReceiveAsync();
        public Task<Message> ReceiveMessageAsync(TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(operationTimeout);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount) => _messageReceiver.ReceiveAsync(maxMessageCount);
        public Task<IList<Message>> ReceiveMessageAsync(int maxMessageCount, TimeSpan operationTimeout) => _messageReceiver.ReceiveAsync(maxMessageCount, operationTimeout);
    }
```

```csharp
    public class MessagesSenderService : IMessagesSenderService
    {
        private readonly ITopicClient _client;
        private readonly ServiceBusSettings _serviceBusSettings;

        public MessagesSenderService(ServiceBusSettings serviceBusSettings)
        {
            _serviceBusSettings = serviceBusSettings;
            var token = TokenProvider.CreateSharedAccessSignatureTokenProvider(_serviceBusSettings.SharedAccessName,
                                                                            _serviceBusSettings.SharedAccessKey, TokenScope.Entity);
            _client = new TopicClient(_serviceBusSettings.ServiceBusNamespace, _serviceBusSettings.TopicName, token);
        }

        public async Task<string> SendMessageAsync(string messageBody)
        {
            var correlationId = Guid.NewGuid().ToString("N");
            var messageToSend = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(messageBody));
            var message = new Message(messageToSend)
            {
                ContentType = $"{System.Net.Mime.MediaTypeNames.Application.Json};charset=utf-8",
                CorrelationId = correlationId
            };
            await _client.SendAsync(message);
            return correlationId;
        }
    }
```

#### [9. Azure Storage Blobs SDK with ASP .NET Core Template](https://github.com/Daniel-Krzyczkowski/AzureDeveloperTemplates/tree/master/src/azure-blob-storage-sdk-asp-net-core-template/AzureDeveloperTemplates.BlobStorage)

Sample project to present how to use Azure Storage Blobs SDK to upload and downlad files from the Azure Blob Storage:

```csharp
    public class StorageService : IStorageService
    {
        private readonly BlobStorageSettings _blobStorageSettings;

        public StorageService(BlobStorageSettings blobStorageSettings)
        {
            _blobStorageSettings = blobStorageSettings;
        }

        public async Task DeleteBlobIfExistsAsync(string blobName)
        {
            var container = await GetBlobContainer();
            var blockBlob = container.GetBlobClient(blobName);
            await blockBlob.DeleteIfExistsAsync();
        }

        public async Task<bool> DoesBlobExistAsync(string blobName)
        {
            var container = await GetBlobContainer();
            var blockBlob = container.GetBlobClient(blobName);
            var doesBlobExist = await blockBlob.ExistsAsync();
            return doesBlobExist.Value;
        }

        public async Task DownloadBlobIfExistsAsync(Stream stream, string blobName)
        {
            var container = await GetBlobContainer();
            var blockBlob = container.GetBlobClient(blobName);

            var doesBlobExist = await blockBlob.ExistsAsync();

            if (doesBlobExist.Value == true)
            {
                await blockBlob.DownloadToAsync(stream);
            }
        }

        public async Task<string> GetBlobUrl(string blobName)
        {
            var container = await GetBlobContainer();
            var blockBlob = container.GetBlobClient(blobName);

            var exists = await blockBlob.ExistsAsync();

            if (exists)
            {
                return blockBlob.Uri.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task UploadBlobAsync(Stream stream, string blobName)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var container = await GetBlobContainer();

            BlobClient blob = container.GetBlobClient(blobName);
            await blob.UploadAsync(stream);
        }

        private async Task<BlobContainerClient> GetBlobContainer()
        {
            BlobContainerClient container = new BlobContainerClient(_blobStorageSettings.ConnectionString,
                                                                            _blobStorageSettings.ContainerName);

            await container.CreateIfNotExistsAsync();

            return container;
        }
    }
```


#### 10. Azure Cognitive Search SDK with ASP .NET Core Template

To be added soon.
