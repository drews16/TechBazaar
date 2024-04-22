using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartController (
        ICartService cartService) : ControllerBase
    {
        [Authorize]
        [HttpGet("{cartId:long}")]
        public async Task<IActionResult> GetCart(long cartId)
        {
            var response = await cartService
                .GetCartAsync(cartId);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}