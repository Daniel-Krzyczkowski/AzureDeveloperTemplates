using AzureDeveloperTemplates.KeyVaultSdk.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.KeyVaultSdk.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecretsController : ControllerBase
    {
        private readonly ISecretManager _secretManager;

        public SecretsController(ISecretManager secretManager)
        {
            _secretManager = secretManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var secret = await _secretManager.GetSecretAsync("TestSecret");
            return Ok(secret);
        }
    }
}
