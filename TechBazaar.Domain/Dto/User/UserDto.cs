namespace TechBazaar.Domain.Dto.User
{
    public sealed record UserDto(string FirstName, string AccessToken, string RefreshToken, long CartId);
}