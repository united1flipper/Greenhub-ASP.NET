using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models
{
	public class OrderPlant
	{
		[Key]
		public int Id { get; set; }
		public int Amount { get; set; }
		public double Price { get; set; }
		public int PlantId { get; set; }
		[ForeignKey("PlantId")]
		public virtual Plant Plant { get; set; }

		public int OrderId { get; set; }
		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }
	}
}
