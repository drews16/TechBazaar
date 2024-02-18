namespace TechBazaar.Domain.Dto.Token
{
    public sealed class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}