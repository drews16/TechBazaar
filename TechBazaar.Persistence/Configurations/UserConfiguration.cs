using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserId");
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(30);
            builder.Property(x => x.Login)
                .IsRequired();
            builder.Property(x => x.Password)
                .IsRequired();
            builder.Property(x => x.CartId)
                .IsRequired();

            builder.HasOne(x => x.Cart)
                .WithOne(x => x.User)
                .HasForeignKey<User>(x => x.CartId);

        }
    }
}
