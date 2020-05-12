using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace AzureDeveloperTemplates.AdB2C.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var userId = new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return $"User id: {userId}";
        }
    }
}
