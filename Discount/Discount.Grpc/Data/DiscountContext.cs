using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> option)
            : base(option) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
               new Coupon { Id = 1, ProductName = "IPhone X", Description = "Iphone Discount", Amount = 150 },
               new Coupon { Id = 2, ProductName = "Samsung", Description = "Samsung Discount", Amount = 100 }
               );
        }
    }
}
