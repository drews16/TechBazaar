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

            services.AddSingleton<PasswordHasherHelper>();

            return services;
        }
    }
}