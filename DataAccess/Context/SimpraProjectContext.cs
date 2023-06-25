using Base.Entities.Concrete;
using Base.Entities.Configurations;
using Entities.Concrete;
using Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class SimpraProjectContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-MP6HDBU; Database=TestOne; Trusted_Connection=true; TrustServerCertificate=True");
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CartItem> Cart { get; set; }
    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OperationClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserOperationClaimConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemConfiguration());
        modelBuilder.ApplyConfiguration(new CouponConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
