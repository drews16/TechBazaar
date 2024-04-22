using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Dto.CartProduct;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/cart-products")]
    public class CartProductsController (
        ICartProductService cartProductService) : ControllerBase
    {
        [Authorize]
        [HttpPost("{cartId:long}/create")]
        public async Task<IActionResult> AddCartProduct(long cartId, CreateCartProductDto dto)
        {
            var response = await cartProductService
                .AddCartProductAsync(cartId, dto);

            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }

        [Authorize]
        [HttpDelete("{cartProductId:long}/remove")]
        public async Task<IActionResult> RemoveCartProduct(long cartProductId)
        {
            var response = await cartProductService
                .RemoveCartProductAsync(cartProductId);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}