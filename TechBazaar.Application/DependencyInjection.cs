using Microsoft.Extensions.DependencyInjection;
using TechBazaar.Application.Services;
using TechBazaar.Domain.Interfaces.Services;

namespace TechBazaar.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}