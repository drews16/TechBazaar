using Microsoft.Extensions.DependencyInjection;
using TechBazaar.Application.Helpers;
using TechBazaar.Application.Services;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartProductService, CartProductService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IOfficeService, OfficeService>();

            services.AddSingleton<PasswordHasherHelper>();

            return services;
        }
    }
}