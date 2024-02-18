using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.Token;
using TechBazaar.Domain.Dto.User;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;
using TechBazaar.Domain.Extensions;
using TechBazaar.Application.Helpers;

namespace TechBazaar.Application.Services
{
    public sealed class AuthService (
       IBaseRepository<User> userRepository,
       IBaseRepository<UserToken> userTokenRepository,  
       PasswordHasherHelper passwordHasher,
       ILogger logger): IAuthService
    {
        public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
        {
            try
            {
                var user = await userRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Login == dto.Login);

                if(user == null)
                {
                    return new BaseResult<TokenDto>
                    {
                        ErrorMessage = "Пользователь не найден"
                    };
                }

                if (!passwordHasher.IsVerifyPassword(dto.Login, dto.Password, user.Password))
                {
                    return new BaseResult<TokenDto>
                    {
                        ErrorMessage = "Неверный логин или пароль"
                    };
                }

                var userToken = await userTokenRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                if(userToken == null)
                {
                    userToken = new UserToken
                    {

                    };
                }

                return null;
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<TokenDto>
                {
                    ErrorMessage = "Произошла внутреняя ошибка сервера"
                };
            }
        }

        public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
        {
            if(dto.Password != dto.PasswordConfirm)
            {
                return new BaseResult<UserDto>
                {
                    ErrorMessage = "Пароли не совпадают"
                };
            }

            try
            {
                var user = await userRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.Login == dto.Login);

                if(user != null)
                {
                    return new BaseResult<UserDto>
                    {
                        ErrorMessage = "Пользователь уже зарегистрирован"
                    };
                }

                user = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Login = dto.Login,
                    Password = passwordHasher.HashPassword(dto.Login, dto.Password),
                    Cart = new Cart()
                };

                await userRepository.InsertAsync(user);

                return new BaseResult<UserDto>
                {
                    Data = user.ToDto()
                };
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<UserDto>
                {
                    ErrorMessage = "Произошла внутрення ошибка сервера"
                };
            }
        }
    }
}