using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Dto.Order;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController (
        IOrderService orderService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserOrders()
        {
            var userId = Guid.Parse(HttpContext?.User?.Identity?.Name);
            
            var response = await orderService
                .GetUserOrdersAsync(userId);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }

        [Authorize]
        [HttpGet("OrderInfo")]
        public async Task<IActionResult> GetOrderInfo([FromQuery] long cartId)
        {
            var response = await orderService
                .GetOrderInfo(cartId);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response.Data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var userId = Guid.Parse(HttpContext?.User?.Identity?.Name);

            dto.UserId = userId;

            var response = await orderService
                .CreateOrderAsync(dto);

            if(!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            return Ok();
        }
    }
}