using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechBazaar.Domain.Entity;

namespace TechBazaar.Persistence.Configurations
{
    public sealed class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserTokenId");
            builder.Property(x => x.RefreshToken)
                .IsRequired();
            builder.Property(x => x.RefreshTokenExpiryTime)
                .IsRequired();
        }
    }
}