using Microsoft.AspNetCore.Mvc;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.ViewModels
{
	public class NewPlantDropdown : Controller
	{
		public NewPlantDropdown()
		{
			Categories = new List<Category>();
		}
		public List<Category> Categories { get; set; }
	}
}
