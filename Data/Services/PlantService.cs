using Microsoft.EntityFrameworkCore;
using PlantShop.Data.Base;
using PlantShop.Data.ViewModels;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.Services
{
    public class PlantService : EntityBaseRepository<Plant>, IPlantService
    {
        private readonly AppDbContext _context;
        public PlantService(AppDbContext context) : base(context)
        {
            _context = context;

        }

		public async Task AddNewPlant(NewPlant data)
		{
            var newPlant = new Plant()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                Quantity = data.Quantity,
                ImageURL = data.ImageURL,

            };
            await _context.Plants.AddAsync(newPlant);
            await _context.SaveChangesAsync();


            foreach (var categoryId in data.CategoryIds)
			{
                var newPlantCategory = new Plant_Category()
                {
                    PlantId = newPlant.Id,
                    CategoryId = categoryId
                };
                await _context.Plants_Categories.AddAsync(newPlantCategory);
			}
            await _context.SaveChangesAsync();
		}

		public async Task<NewPlantDropdown> GetNewPlantCategories()
        {
			var response = new NewPlantDropdown
			{
				Categories = await _context.Categories.OrderBy(n => n.Description).ToListAsync()
			};
			return response;
        }


		public async Task<Plant> GetPlantById(int id)
        {
            var plantDetails = _context.Plants
                .Include(p => p.Plants_Categories).ThenInclude(c => c.Category)
                 .FirstOrDefaultAsync(i => i.Id == id);

            return await plantDetails;
        }
    }
}
