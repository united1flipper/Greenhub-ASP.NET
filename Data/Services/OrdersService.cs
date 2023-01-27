using Microsoft.EntityFrameworkCore;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.Services
{
	public class OrdersService : IOrdersService
	{

		private readonly AppDbContext _context;

		public OrdersService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Order>> GetOrdersByUserId(string userId)
		{
			var orders = await _context.Orders.Include(n => n.OrderPlants).ThenInclude(n => n.Plant).Where(n => n.UserId == userId).ToListAsync();
			return orders;
		
		}

		public async Task StoreOrder(List<ShoppingCardItem> items, string userId, string userEmailAddress)
		{
			var order = new Order()
			{
				UserId = userId,
				Email = userEmailAddress
			};
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			foreach (var item in items)
			{
				var orderPlant = new OrderPlant()
				{
					Amount = item.Amount,
					PlantId = item.Plant.Id,
					OrderId = order.Id,
					Price = item.Plant.Price
				};
				await _context.OrderPlant.AddAsync(orderPlant);
			}
			await _context.SaveChangesAsync();
		}
	}
}
