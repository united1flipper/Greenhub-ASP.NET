using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.ViewModels
{
	public class Register
	{

		[Display(Name = "Full name")]
		[Required(ErrorMessage = "Fullname is required")]
		public string FullName { get; set; }

		[Display(Name = "Email address")]
		[Required(ErrorMessage = "Email is required")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name ="Confirm password")]
		[Required(ErrorMessage ="Confirm your password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage ="Password do not match")]
		public string ConfirmPassword { get; set; }
	}
}
