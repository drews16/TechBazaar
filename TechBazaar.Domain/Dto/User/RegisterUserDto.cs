namespace TechBazaar.Domain.Dto.User
{
    public sealed record RegisterUserDto(string FirstName, string LastName, string Login, string Password, 
        string PasswordConfirm);
}