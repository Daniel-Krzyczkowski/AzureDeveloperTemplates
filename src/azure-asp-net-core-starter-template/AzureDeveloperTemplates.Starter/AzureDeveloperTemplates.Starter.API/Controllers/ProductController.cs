using AzureDeveloperTemplates.Starter.Core.DomainModel;
using AzureDeveloperTemplates.Starter.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureDeveloperTemplates.Starter.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        /// <summary>
        /// Gets list with available products
        /// </summary>
        /// <returns>
        /// List with available products
        /// </returns> 
        /// <response code="200">List with products</response>
        /// <response code="401">Access denied</response>
        /// <response code="404">Products list not found</response>
        /// <response code="500">Oops! something went wrong</response>
        [ProducesResponseType(typeof(IReadOnlyList<Product>), 200)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var allProductsResult = await _productService.GetAllAsync();
            return Ok(allProductsResult);
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <returns>
        /// Returns created product
        /// </returns> 
        /// <param name="product">Product object with properties:<br/>
        /// <br>1. Name - name of the new product</br>
        /// </param>
        /// <response code="200">Added product</response>
        /// <response code="401">Access denied</response>
        /// <response code="400">Model is not valid</response>
        /// <response code="500">Oops! something went wrong</response>
        [ProducesResponseType(typeof(Product), 200)]
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