using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Database
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationType> SpecificationTypes { get; set; }
    }
}