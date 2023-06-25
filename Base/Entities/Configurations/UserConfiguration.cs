using Base.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Entities.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.VirtualWallet).IsRequired(true);
        builder.Property(x => x.EarnedPoints).IsRequired(true);
        builder.Property(x => x.CreatedAt).IsRequired(true);
        builder.Property(x => x.ShippingAddress).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.Status).IsRequired(true).HasMaxLength(25);
        builder.Property(x => x.PasswordSalt)
            .IsRequired(true)
            .HasMaxLength(500)
            .HasColumnType("varbinary(500)");
        builder.Property(x => x.PasswordHash)
            .IsRequired(true)
            .HasMaxLength(500)
            .HasColumnType("varbinary(500)");
    }
}
