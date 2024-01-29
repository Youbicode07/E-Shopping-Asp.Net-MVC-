using Shop_WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop_WebApp.Repository
{
	public interface IUserOrderRepository
	{
		Task<IEnumerable<Order>> UserOrders();
	}
}
