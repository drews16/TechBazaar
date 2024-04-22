using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Persistence.Database;
using TechBazaar.Persistence.Interceptors;
using TechBazaar.Persistence.Repositories;

namespace TechBazaar.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SQLServer")));

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

            services.AddSingleton<DateInterceptor>();

            services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<CartProduct>, BaseRepository<CartProduct>>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();

            return services;
        }
    }
}