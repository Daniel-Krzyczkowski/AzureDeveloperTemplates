using AzureDeveloperTemplates.Starter.API.BackgroundServices.Channels;
using AzureDeveloperTemplates.Starter.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {

        private readonly ILogger<FileController> _logger;
        private readonly FileProcessingChannel _fileProcessingChannel;

        public FileController(ILogger<FileController> logger, FileProcessingChannel fileProcessingChannel)
        {
            _logger = logger;
            _fileProcessingChannel = fileProcessingChannel ?? throw new ArgumentNullException(nameof(fileProcessingChannel));
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Post([FromForm]FilesUpload filesToUpload, CancellationToken cancellationToken)
        {
            if (filesToUpload?.Files == null)
            {
                return BadRequest("No file found to upload");
            }

            long size = filesToUpload.Files.Sum(f => f?.Length ?? 0);
            if (size == 0)
            {
                return BadRequest("No file found to upload");
            }

            foreach (var formFile in filesToUpload.Files)
            {
                var fileTempPath = @$"{Path.GetTempPath()}{formFile.FileName}";

                using (var stream = new FileStream(fileTempPath, FileMode.Create, FileAccess.Write))
                {
                    await formFile.CopyToAsync(stream, cancellationToken);
                }

                var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(TimeSpan.FromSeconds(3));

                try
                {
                    var fileWritten = await _fileProcessingChannel.AddFileAsync(fileTempPath, cts.Token);

                    if (!fileWritten)
                    {
                        _logger.LogError($"An error occurred when processing file: {formFile.FileName}");
                        return StatusCode(500, $"An error occurred when processing file: {formFile.FileName}");
                    }
                }
                catch (OperationCanceledException) when (cts.IsCancellationRequested)
                {
                    System.IO.File.Delete(fileTempPath);
                    throw;
                }
            }

            return Ok();
        }
    }
}
