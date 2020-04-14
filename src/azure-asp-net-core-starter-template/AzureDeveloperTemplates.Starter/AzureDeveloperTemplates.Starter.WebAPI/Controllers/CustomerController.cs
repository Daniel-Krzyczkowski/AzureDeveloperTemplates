using AzureDeveloperTemplates.Starter.Core.DomainModel.Base;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDeveloperTemplates.Starter.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IDataService<IEntity> dataService;

        public CustomerController(IEnumerable<IDataService<IEntity>> dataServices)
        {
            dataService = dataServices.LastOrDefault()
                          ?? throw new ArgumentNullException(nameof(dataService));
        }
    }
}