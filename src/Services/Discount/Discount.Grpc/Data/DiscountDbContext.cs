using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountDbContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "MacBook Pro M2", Description = "MacBook Special Discount", Amount = 250 },
            new Coupon { Id = 2, ProductName = "Sony WH-1000XM5", Description = "Headphones Limited Offer", Amount = 80 },
            new Coupon { Id = 3, ProductName = "Dell XPS 13", Description = "Laptop Seasonal Discount", Amount = 200 },
            new Coupon { Id = 4, ProductName = "iPad Air", Description = "Tablet Promo Discount", Amount = 120 }
        );
    }

}
