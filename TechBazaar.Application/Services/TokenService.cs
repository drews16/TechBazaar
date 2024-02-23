using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TechBazaar.Domain.Dto.Token;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;

namespace TechBazaar.Application.Services
{
    public sealed class TokenService (
        IBaseRepository<User> userRepository,
        IBaseRepository<UserToken> userTokenRepository,
        IConfiguration configuration): ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            int expire = Int32.Parse(configuration["JwtSettings:Expire"]!);

            var jwt = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expire),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)
                        ), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler()
                .WriteToken(jwt);
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(randomNumbers);
            
            return BitConverter.ToString(randomNumbers);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParametrs = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler
                .ValidateToken(token, tokenValidationParametrs, out var securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken || 
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Невалидный токен");
            }

            return claimsPrincipal;
        }

        public async Task<BaseResult<TokenDto>> RefreshToken(string refreshToken)
        {
            var user = await userRepository
                .GetAll()
                .Include(x => x.UserToken)
                .FirstOrDefaultAsync(x => x.UserToken.RefreshToken == refreshToken);

            if (user == null || user.UserToken.RefreshToken != refreshToken
                || user.UserToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new BaseResult<TokenDto>
                {
                    ErrorMessage = "Невалидный запрос от клиента"
                };
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };

            var newAccessToken = GenerateAccessToken(claims);
            var newRefreshToken = GenerateRefreshToken();

            user.UserToken.RefreshToken = newRefreshToken;
            user.UserToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await userRepository.UpdateAsync(user);
            await userTokenRepository.UpdateAsync(user.UserToken);

            return new BaseResult<TokenDto>
            {
                Data = new TokenDto
                (
                    newAccessToken,
                    newRefreshToken
                )
            };
        }

        //public async  Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto)
        //{
        //    string accessToken = dto.AccessToken;
        //    string refreshToken = dto.RefreshToken;

        //    var claimsPrincipal = GetPrincipalFromExpiredToken(accessToken);

        //    var userId = Guid.Parse(claimsPrincipal.Identity?.Name);

        //    var user = await userRepository
        //        .GetAll()
        //        .Include(x => x.UserToken)
        //        .FirstOrDefaultAsync(x => x.Id == userId);

        //    if (user == null || user.UserToken.RefreshToken != refreshToken 
        //        || user.UserToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
        //    {
        //        return new BaseResult<TokenDto>
        //        { 
        //            ErrorMessage = "Невалидный запрос от клиента"
        //        };
        //    }

        //    var newAccessToken = GenerateAccessToken(claimsPrincipal.Claims);
        //    var newRefreshToken = GenerateRefreshToken();

        //    user.UserToken.RefreshToken = newRefreshToken;

        //    await userRepository.UpdateAsync(user);

        //    return new BaseResult<TokenDto>
        //    {
        //        Data = new TokenDto
        //        {
        //            AccessToken = newAccessToken
        //        }
        //    };
        //}
    }
}