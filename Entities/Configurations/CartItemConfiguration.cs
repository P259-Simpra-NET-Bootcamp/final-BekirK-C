using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.CustomerId).IsRequired(true).HasMaxLength(80);
        builder.Property(x => x.ProductId).IsRequired(true);
        builder.Property(x => x.ProductName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.UnitPrice).IsRequired(true);
        builder.Property(x => x.Quantity).IsRequired(true);
        builder.Property(x => x.TotalPrice).IsRequired(true);
        builder.Property(x => x.CreatedAt).IsRequired(true);
    }
}