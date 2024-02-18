using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public sealed class ProductController (
        IProductService productService): ControllerBase
    {
        [HttpGet("{category:alpha}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var response = await productService
                .GetProductsAsync(category);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{productId:long}")]
        public async Task<IActionResult> GetProductDetails(long productId)
        {
            var response = await productService
                .GetProductDetailsAsync(productId);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}