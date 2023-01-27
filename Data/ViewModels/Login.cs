using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.ViewModels
{
	public class Login
	{
		[Display(Name = "Email address")]
		[Required(ErrorMessage = "Email is required")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
