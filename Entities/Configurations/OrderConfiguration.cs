using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();
        builder.Property(x => x.ProductId).IsRequired(true);
        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.OrderId).IsRequired(true);
        builder.Property(x => x.CouponCode).IsRequired(false);
        builder.Property(x => x.CouponAmount).IsRequired(false);
        builder.Property(x => x.SpendPoint).IsRequired(false);
        builder.Property(x => x.ShippingAddress).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.OrderDate).IsRequired(true);
        builder.Property(x => x.Quantity).IsRequired(true);
        builder.Property(x => x.TotalPrice).IsRequired(true);
    }
}
