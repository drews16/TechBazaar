using TechBazaar.Domain.Dto.Token;
using TechBazaar.Domain.Dto.User;
using TechBazaar.Domain.Result;

namespace TechBazaar.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserDto>> RegisterAsync(RegisterUserDto dto);
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<UserDto>> LoginAsync(LoginUserDto dto);
    }
}