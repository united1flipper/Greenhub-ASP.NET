using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlantShop.Data;
using PlantShop.Data.ViewModels;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly AppDbContext _context;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;

		}
		public IActionResult Login()
		{

			var response = new Login();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Login(Login login)
		{
			if (!ModelState.IsValid) return View(login);

			var user = await _userManager.FindByEmailAsync(login.EmailAddress);
			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
				if (passwordCheck)
				{
					var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Plants");
					}
				}
				TempData["Error"] = "Wrong credentials. Please try again";
				return View(login);
			}

			TempData["Error"] = "Wrong credentials. Please try again";
			return View(login);
		}
		public IActionResult Register()
		{

			var response = new Register();
			return View(response);
		}

		[HttpPost]
		public async Task<IActionResult> Register(Register register)
		{
			if (!ModelState.IsValid) return View(register);

			var user = await _userManager.FindByIdAsync(register.EmailAddress);
			if (user != null)
			{
				TempData["Error"] = "Email address already in use";
				return View(register);
			}

			var newUser = new User()
			{
				FullName = register.FullName,
				Email = register.EmailAddress,
				UserName = register.EmailAddress

			};

			var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);
			if (newUserResponse.Succeeded)
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
				return View("RegisterCompleted");
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Plants");
		}
	}
}