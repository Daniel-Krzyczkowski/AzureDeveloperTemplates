using Azure;
using Azure.Storage.Blobs;
using AzureDeveloperTemplates.Starter.Infrastructure.Configuration.Interfaces;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage.Interfaces;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorageServiceConfiguration _storageServiceConfiguration;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<StorageService> _logger;
        public StorageService(IStorageServiceConfiguration storageServiceConfiguration,
                              BlobServiceClient blobServiceClient,
                              ILogger<StorageService> logger)
        {
            _storageServiceConfiguration = storageServiceConfiguration;
            _blobServiceClient = blobServiceClient;
            _logger = logger;
        }

        public async Task DeleteBlobIfExistsAsync(string blobName)
        {
            try
            {
                var container = await GetBlobContainer();
                var blockBlob = container.GetBlobClient(blobName);
                await blockBlob.DeleteIfExistsAsync();
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Document {blobName} was not deleted successfully - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DoesBlobExistAsync(string blobName)
        {
            try
            {
                var container = await GetBlobContainer();
                var blockBlob = container.GetBlobClient(blobName);
                var doesBlobExist = await blockBlob.ExistsAsync();
                return doesBlobExist.Value;
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Document {blobName} existence cannot be verified - error details: {ex.Message}");
                throw;
            }
        }

        public async Task DownloadBlobIfExistsAsync(Stream stream, string blobName)
        {
            try
            {
                var container = await GetBlobContainer();
                var blockBlob = container.GetBlobClient(blobName);

                var doesBlobExist = await blockBlob.ExistsAsync();

                if (doesBlobExist.Value == true)
                {
                    await blockBlob.DownloadToAsync(stream);
                }
            }

            catch (RequestFailedException ex)
            {
                _logger.LogError($"Cannot download document {blobName} - error details: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetBlobUrl(string blobName)
        {
            try
            {
                var container = await GetBlobContainer();
                var blockBlob = container.GetBlobClient(blobName);

                var exists = await blockBlob.ExistsAsync();

                string blobUrl = exists ? blockBlob.Uri.ToString() : string.Empty;
                return blobUrl;
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Url for document {blobName} was not found - error details: {ex.Message}");
                throw;
            }
        }

        public async Task UploadBlobAsync(Stream stream, string blobName)
        {
            try
            {
                stream.Seek(0, SeekOrigin.Begin);
                var container = await GetBlobContainer();

                BlobClient blob = container.GetBlobClient(blobName);
                await blob.UploadAsync(stream);
            }

            catch (RequestFailedException ex)
            {
                _logger.LogError($"Document {blobName} was not uploaded successfully - error details: {ex.Message}");
                throw;
            }
        }

        private async Task<BlobContainerClient> GetBlobContainer()
        {
            try
            {
                BlobContainerClient container = _blobServiceClient
                                .GetBlobContainerClient(_storageServiceConfiguration.ContainerName);

                await container.CreateIfNotExistsAsync();

                return container;
            }
            catch (RequestFailedException ex)
            {
                _logger.LogError($"Cannot find blob container: {_storageServiceConfiguration.ContainerName} - error details: {ex.Message}");
                throw;
            }
        }
    }
}
