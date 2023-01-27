using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models
{
	public class ShoppingCardItem
	{
		[Key]
	public int Id { get; set; }
	public Plant Plant { get; set; }

		public int Amount { get; set; }

		public string ShoppingCardId { get; set; }
	}
}
