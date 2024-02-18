using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("CategoryId");
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.UrlName)
                .IsRequired()
                .HasMaxLength(15);
        }
    }
}