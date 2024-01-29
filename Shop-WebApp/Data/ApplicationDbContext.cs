using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shop_WebApp.Models;

namespace Shop_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Shop_WebApp.Models.Product> Product { get; set; } = default!;
        public DbSet<Shop_WebApp.Models.ProductTypes> ProductTypes { get; set; } = default!;
	 
		public DbSet<Shop_WebApp.Models.ShoppingCart> ShoppingCarts { get; set; } = default!;
		public DbSet<Shop_WebApp.Models.CartDetail> CartDetails { get; set; } = default!;
		public DbSet<Shop_WebApp.Models.Order> Orders { get; set; } = default!;
		public DbSet<Shop_WebApp.Models.OrderDetail> OrderDetails { get; set; } = default!;

		public DbSet<Shop_WebApp.Models.OrderStatus> orderStatuses { get; set; } = default!;
	 
	}
}
