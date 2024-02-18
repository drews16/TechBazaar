using System.Security.Cryptography;
using System.Text;

namespace TechBazaar.Application.Helpers
{
    public sealed class PasswordHasherHelper
    {
        public string HashPassword(string login, string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(login + password));

            return BitConverter.ToString(bytes);
        }

        public bool IsVerifyPassword(string providedPassword, string providedLogin, string userPasswordHash)
        {
            return HashPassword(providedLogin, providedPassword) == userPasswordHash;
        }
    }
}