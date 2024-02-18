using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("CartId");
            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasMany(x => x.CartProducts)
                .WithOne(x => x.Cart)
                .HasForeignKey(x => x.CartId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}