using AzureDeveloperTemplates.Starter.Infrastructure.Services.Storage.Interfaces;
using AzureDeveloperTemplates.Starter.API.BackgroundServices.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
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
                    Log.ProcessedMessage(_logger);
                }
                finally
                {
                    File.Delete(filePath);
                }
            }
        }

        internal static class EventIds
        {
            public static readonly EventId StartedProcessing = new EventId(100, "StartedProcessing");
            public static readonly EventId ProcessedMessage = new EventId(110, "ProcessedMessage");
        }

        private static class Log
        {
            private static readonly Action<ILogger, string, Exception> _processedMessage = LoggerMessage.Define<string>(
                LogLevel.Debug,
                EventIds.ProcessedMessage,
                "Read and processed message with ID '{MessageId}' from the channel.");

            public static void ProcessedMessage(ILogger logger) => logger.Log(LogLevel.Trace, EventIds.ProcessedMessage, "Processed message successfully");
        }
    }
}
