using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AzureDeveloperTemplates.QueueStorage.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {

        private readonly ILogger<QueueController> _logger;

        public QueueController(ILogger<QueueController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
