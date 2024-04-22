using Microsoft.AspNetCore.Authorization;
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
                .RegisterAsync(dto);

            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            HttpContext.Response.Cookies.Append("AspNetCore.Application.Id", response.Data.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
            HttpContext.Response.Cookies.Append("AspNetCore.Application.LongId", response.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { userName = response.Data.FirstName, cartId = response.Data.CartId });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var response = await authService
                .LoginAsync(dto);

            if (!response.IsSuccess)
            {
                return BadRequest(response.ErrorMessage);
            }

            HttpContext.Response.Cookies.Append("AspNetCore.Application.Id", response.Data.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
            HttpContext.Response.Cookies.Append("AspNetCore.Application.LongId", response.Data.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new { userName = response.Data.FirstName, cartId = response.Data.CartId });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            if (HttpContext.Request.Cookies.ContainsKey("AspNetCore.Application.Id"))
            {
                HttpContext.Response.Cookies.Append("AspNetCore.Application.Id", " ", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(-10d)
                });
            }
            
            if(HttpContext.Request.Cookies.ContainsKey("AspNetCore.Application.LongId"))
            {
                HttpContext.Response.Cookies.Append("AspNetCore.Application.LongId", " ", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(-10d)
                });
            }

            return Ok();
        }
    }
}