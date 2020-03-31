using Azure.Storage.Blobs;
using AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Settings;
using AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.AzureCoreExtensions.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobStorageSettings _blobStorageSettings;

        public FileController(BlobServiceClient blobServiceClient, BlobStorageSettings blobStorageSettings)
        {
            _blobServiceClient = blobServiceClient;
            _blobStorageSettings = blobStorageSettings;
        }

        [HttpGet]
        public async Task Get([FromQuery] string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);
            await blobContainer.CreateIfNotExistsAsync();

            var blockBlob = blobContainer.GetBlobClient(fileName);

            var doesBlobExist = await blockBlob.ExistsAsync();

            if (doesBlobExist.Value == true)
            {
                await blockBlob.DownloadToAsync(Response.Body);
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Post([FromForm]FilesUpload filesToUpload)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);
            await blobContainer.CreateIfNotExistsAsync();

            long size = filesToUpload.Files.Sum(f => f?.Length ?? 0);
            if (filesToUpload?.Files == null || size == 0)
                return BadRequest("No file to upload found");

            foreach (var formFile in filesToUpload.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    BlobClient blob = blobContainer.GetBlobClient(formFile.FileName);
                    await blob.UploadAsync(memoryStream);
                }
            }

            return Created("Get", null);
        }

        [HttpDelete]
        [Route("{fileName}")]
        public async Task<IActionResult> Delete([FromRoute] string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);
            await blobContainer.CreateIfNotExistsAsync();

            var blockBlob = blobContainer.GetBlobClient(fileName);
            await blockBlob.DeleteIfExistsAsync();

            return NoContent();
        }
    }
}
