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
        private readonly IProductLocationService _productLocationService;

        public ProductController(IProductService productService, IProductLocationService productLocationService)
        {
            _productService = productService
                          ?? throw new ArgumentNullException(nameof(productService));
            _productLocationService = productLocationService
                          ?? throw new ArgumentNullException(nameof(productLocationService));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProductsResult = await _productService.GetAllAsync();
            return Ok(allProductsResult);
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddNewProduct([FromBody] Product product)
        {
            var newProductResult = await _productService.AddNewAsync(product);
            return Ok(newProductResult);
        }

        [HttpGet("location/all")]
        public async Task<IActionResult> GetAllProductsLocations()
        {
            var allProductsLocationsResult = await _productLocationService.GetAllAsync();
            return Ok(allProductsLocationsResult);
        }

        [HttpPost("location/new")]
        public async Task<IActionResult> AddNewProductLocation([FromBody] ProductLocation productLocation)
        {
            var newProductLocationResult = await _productLocationService.AddNewAsync(productLocation);
            return Ok(newProductLocationResult);
        }
    }
}