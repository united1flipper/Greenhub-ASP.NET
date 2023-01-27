using Microsoft.EntityFrameworkCore;
using PlantShop.Data.Base;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data.Services
{
    public class CategoryService : EntityBaseRepository<Category>, ICategoryService
    {
        public CategoryService(AppDbContext context) : base(context) { }

    }
}
