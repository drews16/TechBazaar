using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Dto.User;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController (
        IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var response = await authService
                .Register(dto);

            if(!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            return Ok();
        }
    }
}