using System.Security.Claims;
using TechBazaar.Domain.Dto.Token;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        //Task<BaseResult<TokenDto>> RefreshToken(TokenDto dto);
        Task<BaseResult<TokenDto>> RefreshToken(string refreshToken);
    }
}