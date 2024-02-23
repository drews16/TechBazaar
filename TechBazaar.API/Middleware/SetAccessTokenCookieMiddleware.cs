using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TechBazaar.API.Middleware
{
    public sealed class SetAccessTokenCookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public SetAccessTokenCookieMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["AspNetCore.Application.Id"];

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var principal = new JwtSecurityTokenHandler().ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _configuration["JwtSettings:Issuer"],
                        ValidAudience = _configuration["JwtSettings:Audience"],
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!))
                    }, out _);

                    context.User = principal;
                    context.Request?.Headers?.Add("Authorization", "Bearer " + token);
                }
                catch (SecurityTokenException)
                {
                    context.Response.StatusCode = 401;
                }
            }

            await _next(context);
        }
    }
}