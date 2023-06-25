using Base.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.Entities.Configurations;

public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
{
    public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.UserId).IsRequired(true);
        builder.Property(x => x.OperationClaimId).IsRequired(true);
        builder.Property(x => x.UserMail).IsRequired(true).HasMaxLength(50);
    }
}
