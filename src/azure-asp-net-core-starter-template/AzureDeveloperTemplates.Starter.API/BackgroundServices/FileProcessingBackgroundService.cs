using AzureDeveloperTemplates.Starter.API.BackgroundServices.Channels;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.BackgroundServices
{
    public class FileProcessingBackgroundService : BackgroundService
    {
        private readonly ILogger<FileProcessingBackgroundService> _logger;
        private readonly FileProcessingChannel _fileProcessingChannel;
        private readonly IStorageService _storageService;

        public FileProcessingBackgroundService(
            ILogger<FileProcessingBackgroundService> logger,
            FileProcessingChannel boundedMessageChannel,
            IStorageService storageService)
        {
            _logger = logger;
            _fileProcessingChannel = boundedMessageChannel;
            _storageService = storageService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var filePath in _fileProcessingChannel.ReadAllAsync())
            {
                try
                {
                    await using var stream = File.OpenRead(filePath);

                    await _storageService.UploadBlobAsync(stream, Path.GetFileName(filePath));
                    _logger.LogInformation($"File {Path.GetFileName(filePath)} successfully processed");
                }
                finally
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
