using Microsoft.EntityFrameworkCore;
using Serilog;
using TechBazaar.Domain.Dto.User;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Domain.Interfaces.Services;
using TechBazaar.Domain.Result;
using TechBazaar.Application.Helpers;
using System.Security.Claims;

namespace TechBazaar.Application.Services
{
    public sealed class AuthService (
       IBaseRepository<User> userRepository,
       IBaseRepository<UserToken> userTokenRepository,
       ITokenService tokenService,
       PasswordHasherHelper passwordHasher,
       ILogger logger): IAuthService
    {
        public async Task<BaseResult<UserDto>> LoginAsync(LoginUserDto dto)
        {
            try
            {
                var user = await userRepository
                    .GetAll()
                    .Include(x => x.Cart)
                    .FirstOrDefaultAsync(x => x.Login == dto.Login);

                if(user == null)
                {
                    return new BaseResult<UserDto>
                    {
                        ErrorMessage = "Пользователь не найден"
                    };
                }

                if (!passwordHasher.IsVerifyPassword(dto.Login, dto.Password, user.Password))
                {
                    return new BaseResult<UserDto>
                    {
                        ErrorMessage = "Неверный логин или пароль"
                    };
                }

                var userToken = await userTokenRepository
                    .GetAll()
                    .FirstOrDefaultAsync(x => x.UserId == user.Id);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                };

                var accessToken = tokenService.GenerateAccessToken(claims);
                var refreshToken = tokenService.GenerateRefreshToken();

                userToken.RefreshToken = refreshToken;
                userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
               
                await userTokenRepository.UpdateAsync(userToken); 

                return new BaseResult<UserDto>
                {
                    Data = new UserDto
                    ( 
                        user.FirstName,
                        accessToken,
                        refreshToken,
                        user.Cart.Id
                    )
                };
            }
            catch(Exception ex)
            {
                logger.Error(ex, ex.Message);

                return new BaseResult<UserDto>
                {
                    ErrorMessage = "Произошла внутреняя ошибка сервера"
                };
            }
        }

        public async Task<BaseResult<UserDto>> RegisterAsync(RegisterUserDto dto)
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
                    .Include(x => x.Cart)
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

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                };

                var accessToken = tokenService.GenerateAccessToken(claims);
                var refreshToken = tokenService.GenerateRefreshToken();

                var userToken = new UserToken
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
                };

                await userTokenRepository.InsertAsync(userToken);

                return new BaseResult<UserDto>
                {
                    Data = new UserDto
                    (
                        user.FirstName,
                        accessToken,
                        refreshToken,
                        user.Cart.Id
                    )
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