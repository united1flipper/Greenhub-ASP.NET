using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.Services
{
	public interface IOrdersService
	{
		Task StoreOrder(List<ShoppingCardItem> items, string userId, string userEmailAddress);
		Task<List<Order>> GetOrdersByUserId(string userId);
	}
}
