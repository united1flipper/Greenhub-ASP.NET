using Microsoft.AspNetCore.Mvc;
using PlantShop.Data;
using PlantShop.Data.Services;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCategories = await _service.GetAll();
            return View(allCategories);
        }

        //GET: CATEGORIES/CREATE 
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Icon,Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _service.Add(category);
            return RedirectToAction(nameof(Index));
        }

        //GET: CATEGORIES/EDIT

        public async Task<IActionResult> Edit(int id)
        {
            var categoryDetails = await _service.GetById(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Icon,Description")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _service.Update(id, category);
            return RedirectToAction(nameof(Index));
        }

        //GET: CATEGORIES/DELETE

        public async Task<IActionResult> Delete(int id)
        {
            var categoryDetails = await _service.GetById(id);
            if (categoryDetails == null) return View("NotFound");
            return View(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {

            var categoryDetails = await _service.GetById(id);
            if (categoryDetails == null) return View("NotFound");

            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
