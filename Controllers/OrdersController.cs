using Microsoft.AspNetCore.Mvc;
using PlantShop.Data.Services;
using PlantShop.Data.Card;
using PlantShop.Data.ViewModels;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
	public class OrdersController : Controller
	{
		private readonly IPlantService _plantService;
		private readonly ShoppingCard _shoppingCard;
		private readonly IOrdersService _ordersService;

		public OrdersController(IPlantService plantService, ShoppingCard shoppingCard, IOrdersService ordersService)
		{
			_plantService = plantService;
			_shoppingCard = shoppingCard;
			_ordersService = ordersService;
		}
		public IActionResult ShoppingCard()
		{
			var items = _shoppingCard.GetShoppingCardItems();
			_shoppingCard.ShoppingCardItems = items;
			var response = new ShoppingCardViewModel()
			{
				ShoppingCard = _shoppingCard,
				ShoppingCardTotal = _shoppingCard.GetShoppingCardTotal()
			};
			return View(response);
		}

		public async Task<IActionResult> AddItemToShoppingCard(int id)
		{
			var plant = await _plantService.GetPlantById(id);

			if (plant!=null)
			{
				_shoppingCard.AddItemToCard(plant);
			}

			return RedirectToAction(nameof(ShoppingCard));
		}


		public async Task<IActionResult> RemoveItemFromShoppingCard(int id)
		{
			var plant = await _plantService.GetPlantById(id);

			if (plant != null)
			{
				_shoppingCard.RemoveItemFromCard(plant);
			}

			return RedirectToAction(nameof(ShoppingCard));
		}

		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingCard.GetShoppingCardItems();
			string userId = "";
			string userEmailAddress = "";

			await _ordersService.StoreOrder(items, userId, userEmailAddress);
			await _shoppingCard.ClearShoppingCard();
			return View("OrderCompleted");
		}
	}
}
