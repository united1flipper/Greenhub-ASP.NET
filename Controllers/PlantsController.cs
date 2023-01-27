using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantShop.Data;
using PlantShop.Data.Services;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
    public class PlantsController : Controller
    {
        private readonly IPlantService _service;

        public PlantsController(IPlantService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allPlants = await _service.GetAll();
            return View(allPlants);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allPlants = await _service.GetAll();

            if(!string.IsNullOrEmpty(searchString)) {
                var filteredResult = allPlants.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index", filteredResult);
			}

            return View("Index", allPlants);
        }

        //GET: PLANTS/DESCRIPTION

        public async Task<IActionResult> Description(int id)
        {
            var plantDescription = await _service.GetPlantById(id);
            return View(plantDescription);
        }

        //GET: PLANTS/ CREATE

        public async Task<IActionResult> Create()
		{
            var plantDropdownData = await _service.GetNewPlantCategories();

            ViewBag.Categories = new SelectList(plantDropdownData.Categories, "Id", "Description");

            return View();
		}
        public async Task<IActionResult> Edit(int id)
        {
            var plant = await _service.GetById(id);
            if (plant == null) return View("NotFound");
            return View(plant);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return View(plant);
            }
            await _service.Update(id, plant);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPlant plant)
        {
            if (!ModelState.IsValid)
			{
                var plantDropdownData = await _service.GetNewPlantCategories();

                ViewBag.Categories = new SelectList(plantDropdownData.Categories, "Id", "Description");

                return View(plant);
			}

            await _service.AddNewPlant(plant);
            return RedirectToAction(nameof(Index));

        }
    }
}