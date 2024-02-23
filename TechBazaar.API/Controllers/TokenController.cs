using Microsoft.AspNetCore.Mvc;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.API.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController (
        ITokenService tokenService): ControllerBase
    {
        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            string refreshToken = HttpContext.Request.Cookies["AspNetCore.Application.LongId"]!;

            var response = await tokenService.RefreshToken(refreshToken);
            
            if(!response.IsSuccess)
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

                if (HttpContext.Request.Cookies.ContainsKey("AspNetCore.Application.LongId"))
                {
                    HttpContext.Response.Cookies.Append("AspNetCore.Application.LongId", " ", new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(-10d)
                    });
                }

                return BadRequest(response);
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


            return Ok();
        }
    }
}