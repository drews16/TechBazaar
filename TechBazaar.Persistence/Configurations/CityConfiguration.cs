using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("CityId");
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasMany(x => x.Offices)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId);
        }
    }
}