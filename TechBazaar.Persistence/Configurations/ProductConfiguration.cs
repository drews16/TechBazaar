using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductId");
            builder.Property(x => x.Model)
                .IsRequired()
                .HasMaxLength(85);
            builder.Property(x => x.Price)
                .IsRequired();
            builder.Property(x => x.MainImage)
                .IsRequired();
            builder.Property(x => x.CountPurchase)
                .IsRequired();
           builder.Property(x => x.AvailableQuantity)
                .IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x => x.Id);
            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.BrandId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany(x => x.Specifications)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany(x => x.ProductImages)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}