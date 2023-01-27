using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.Card
{
	public class ShoppingCard
	{
		public AppDbContext _context{ get; set; }

		public string ShoppingCardId { get; set; }
		public List<ShoppingCardItem> ShoppingCardItems{ get; set; }

	public ShoppingCard(AppDbContext context)
		{
			_context = context;
		}
		public static ShoppingCard GetShoppingCard(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<AppDbContext>();
			string cardId = session.GetString("CardId") ?? Guid.NewGuid().ToString();
			session.SetString("CardId", cardId);
			return new ShoppingCard(context) { ShoppingCardId = cardId };
		}

		public void AddItemToCard(Plant plant)
		{
			var shoppingCardItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Plant.Id == plant.Id && n.ShoppingCardId == ShoppingCardId);
			if (shoppingCardItem == null)
			{
				shoppingCardItem = new ShoppingCardItem()
				{
					ShoppingCardId = ShoppingCardId,
					Plant = plant,
					Amount = 1
				};

				_context.ShoppingCardItems.Add(shoppingCardItem);
			}
			else
			{
				shoppingCardItem.Amount++;
			}
			_context.SaveChanges();

		}
		public void RemoveItemFromCard(Plant plant)
		{
			var shoppingCardItem = _context.ShoppingCardItems.FirstOrDefault(n => n.Plant.Id == plant.Id && n.ShoppingCardId == ShoppingCardId);
			if (shoppingCardItem != null)
			{
				if (shoppingCardItem.Amount > 1)
				{
					shoppingCardItem.Amount--;
				}
				else
				{
					_context.ShoppingCardItems.Remove(shoppingCardItem);
				}
			}

			_context.SaveChanges();
		}
		public List<ShoppingCardItem> GetShoppingCardItems()
		{
			return ShoppingCardItems ?? 
				(ShoppingCardItems = _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCardId).Include(n => n.Plant).ToList());
		}

		public double GetShoppingCardTotal()
		{
			var total = _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCardId).Select(n => n.Plant.Price * n.Amount).Sum();
			return total; 
		}

		public async Task ClearShoppingCard()
		{
			var items = await _context.ShoppingCardItems.Where(n => n.ShoppingCardId == ShoppingCardId).ToListAsync();
			_context.ShoppingCardItems.RemoveRange(items);
			await _context.SaveChangesAsync();
		}


	}
}
