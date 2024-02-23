using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public sealed class ProductController (
        IProductService productService): ControllerBase
    {
        [HttpGet("{category:alpha}")]
        public async Task<IActionResult> GetProductsByCategory(string category, string? orderByPrice, string? orderByPopularity)
        {
            var response = await productService
                .GetProductsAsync(category, orderByPrice, orderByPopularity);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }

        [HttpGet("best-selling-products/{count:int}")]
        public async Task<IActionResult> GetBestSellingProducts(int count)
        {
            var response = await productService
                .GetBestSellingProducts(count);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }
            
            return Ok(response.Data);
        }

        [HttpGet("{productId:long}")]
        public async Task<IActionResult> GetProductDetails(long productId)
        {
            var response = await productService
                .GetProductDetailsAsync(productId);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}