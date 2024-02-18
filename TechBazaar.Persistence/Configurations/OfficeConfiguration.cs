using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("OfficeId");
            builder.Property(x => x.CityId)
                .IsRequired();
            builder.Property(x => x.Address)
                .IsRequired();
        }
    }
}