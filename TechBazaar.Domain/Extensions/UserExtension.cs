using TechBazaar.Domain.Dto.User;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Domain.Extensions
{
    public static class UserExtension
    {
        public static UserDto ToDto(this User entity)
        {
            return new UserDto
            (
                entity.Id,
                entity.FirstName
            );
        }
    }
}