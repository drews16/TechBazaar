using System.Security.Claims;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.Application.Services
{
    public sealed class TokenService : ITokenService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            throw new NotImplementedException();
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[32];
            // todo: Добавить генерацию.
            return "";
        }
    }
}
