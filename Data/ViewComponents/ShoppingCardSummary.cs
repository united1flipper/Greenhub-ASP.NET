using Microsoft.AspNetCore.Mvc;
using PlantShop.Data.Card;

namespace PlantShop.Data.ViewComponents
{
	public class ShoppingCardSummary : ViewComponent
	{
		private readonly ShoppingCard _shoppingCard;

		public ShoppingCardSummary(ShoppingCard shoppingCard)

		{
			_shoppingCard = shoppingCard;
		}

		public IViewComponentResult Invoke()
		{
			var items = _shoppingCard.GetShoppingCardItems();
			return View(items.Count);
		}

	}
}
