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
    public class UserController : ControllerBase
    {
        private readonly IDataService<IEntity> dataService;

        public UserController(IEnumerable<IDataService<IEntity>> dataServices)
        {
            dataService = dataServices.LastOrDefault()
                          ?? throw new ArgumentNullException(nameof(dataService));
        }
    }
}