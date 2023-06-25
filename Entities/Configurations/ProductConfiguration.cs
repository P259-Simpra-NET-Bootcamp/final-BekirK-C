using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        builder.Property(x => x.Description).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.CategoryId).IsRequired(true);
        builder.Property(x => x.Price).IsRequired(true);
        builder.Property(x => x.MaxPoint).IsRequired(true);
        builder.Property(x => x.PointPercentage).IsRequired(true);
        builder.Property(x => x.Stock).IsRequired(true);
        builder.Property(x => x.CreatedAt).IsRequired(true);

        builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
    }
}
