using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureDeveloperTemplates.BlobStorage.Infrastructure.Services.Interfaces;
using AzureDeveloperTemplates.BlobStorage.WebAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureDeveloperTemplates.BlobStorage.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public FileController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        public async Task Get([FromQuery] string fileName)
        {
            await _storageService.DownloadBlobIfExistsAsync(Response.Body, fileName);
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Post([FromForm]FilesUpload filesToUpload)
        {
            long size = filesToUpload.Files.Sum(f => f?.Length ?? 0);
            if (filesToUpload?.Files == null || size == 0)
                return BadRequest("No file to upload found");

            foreach (var formFile in filesToUpload.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    await _storageService.UploadBlobAsync(memoryStream, formFile.FileName);
                }
            }

            return Created("Get", null);
        }

        [HttpDelete]
        [Route("{fileName}")]
        public async Task<IActionResult> Delete([FromRoute] string fileName)
        {
            await _storageService.DeleteBlobIfExistsAsync(fileName);

            return NoContent();
        }
    }
}