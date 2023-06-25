using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.CreatedAt).IsRequired(true);

        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}
