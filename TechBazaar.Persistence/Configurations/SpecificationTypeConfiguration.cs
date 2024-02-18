using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class SpecificationTypeConfiguration : IEntityTypeConfiguration<SpecificationType>
    {
        public void Configure(EntityTypeBuilder<SpecificationType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("SpecificationTypeId");
            builder.Property(x => x.SpecificationName)
                .IsRequired()
                .HasMaxLength(40);
            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.EngUnit)
                .HasMaxLength(5);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.SpecificationTypes)
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}