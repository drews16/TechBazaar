using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderId");
            builder.Property(x => x.Status)
                .IsRequired();
            builder.Property(x => x.TotalPrice)
                .IsRequired();
            builder.Property(x => x.UserId)
                .IsRequired();
            builder.Property(x => x.OfficeId)
                .IsRequired();

            builder.HasOne(x => x.Office)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.OfficeId);
            builder.HasMany(x => x.OrderProducts)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
        }
    }
}