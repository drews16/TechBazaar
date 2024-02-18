using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechBazaar.Domain.Entity;
using TechBazaar.Domain.Interfaces.Repositories;
using TechBazaar.Persistence.Database;
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

            //services.AddSingleton<DateInterceptor>();

            services.AddScoped<IBaseRepository<Brand>, BaseRepository<Brand>>();
            services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            services.AddScoped<IBaseRepository<Category>, BaseRepository<Category>>();
            services.AddScoped<IBaseRepository<ProductImage>, BaseRepository<ProductImage>>();
            services.AddScoped<IBaseRepository<Specification>, BaseRepository<Specification>>();
            services.AddScoped<IBaseRepository<SpecificationType>, BaseRepository<SpecificationType>>();

            return services;
        }
    }
}