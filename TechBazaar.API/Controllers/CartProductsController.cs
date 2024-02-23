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
        [HttpGet("{cartId:long}")]
        public async Task<IActionResult> GetCartProducts(long cartId)
        {
            var response = await cartProductService
                .GetCartProducts(cartId);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }

        [Authorize]
        [HttpPost("{cartId:long}/create")]
        public async Task<IActionResult> AddProductInCart(long cartId, CreateCartProductDto dto)
        {
            var response = await cartProductService
                .AddProductInCart(cartId, dto);

            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}