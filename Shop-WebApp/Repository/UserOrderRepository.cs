using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop_WebApp.Data;
using Shop_WebApp.Models;

namespace Shop_WebApp.Repository
{
	public class UserOrderRepository : IUserOrderRepository
	{
		private readonly ApplicationDbContext _db;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<IdentityUser> userManager;


		public UserOrderRepository(ApplicationDbContext db,
			UserManager<IdentityUser> userManager,
			 IHttpContextAccessor httpContextAccessor)
		{
			this._db = db;
			this._httpContextAccessor = httpContextAccessor;
			this.userManager = userManager;
		}
        public async Task<IEnumerable<Order>> UserOrders()
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User is not logged in");
                }

                var orders = await _db.Orders
                    .Include(x => x.OrderStatus)
                    .Include(x => x.OrderDetail)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.ProductTypes)
                    .Where(a => a.UserId == userId)
                    .ToListAsync();

                return orders;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception in UserOrders: {ex.Message}");
                throw;
            }
        }

        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal);
            return userId;
        }
    }
}
