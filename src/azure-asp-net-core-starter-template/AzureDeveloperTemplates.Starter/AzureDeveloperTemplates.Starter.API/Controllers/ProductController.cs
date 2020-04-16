using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.Controllers
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
        public async Task<IActionResult> GetAllProducts()
        {
            var allProductsResult = await _productService.GetAllAsync();
            return Ok(allProductsResult);
        }

        [HttpPost()]
        public async Task<IActionResult> AddNewProduct([FromBody] Product product)
        {
            var newProductResult = await _productService.AddNewAsync(product);
            return Ok(newProductResult);
        }
    }
}