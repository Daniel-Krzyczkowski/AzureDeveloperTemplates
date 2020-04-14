using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService
                          ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var allProducts = await _productService.GetAllAsync();
            return Ok(allProducts);
        }
    }
}