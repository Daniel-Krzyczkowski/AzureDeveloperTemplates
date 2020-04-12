using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Infrastructure.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDeveloperTemplates.Starter.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataService<IEntity> dataService;

        public ProductController(IEnumerable<IDataService<IEntity>> dataServices)
        {
            dataService = dataServices.FirstOrDefault()
                          ?? throw new ArgumentNullException(nameof(dataService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}