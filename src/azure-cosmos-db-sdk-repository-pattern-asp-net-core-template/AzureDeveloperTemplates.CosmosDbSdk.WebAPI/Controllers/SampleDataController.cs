using AzureDeveloperTemplates.CosmosDbSdk.Core.Entities;
using AzureDeveloperTemplates.CosmosDbSdk.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.CosmosDbSdk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleDataController : ControllerBase
    {
        private readonly IAsyncRepository<SampleEntity> _sampleRepository;

        public SampleDataController(IAsyncRepository<SampleEntity> sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allItems = await _sampleRepository.ListAllAsync();
            return Ok(allItems);
        }
    }
}
