using PlantShop.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models
{
    public class Plant : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        //RELATIONSHIP
        public List<Plant_Category> Plants_Categories { get; set; }

        //?????
        
        //public ICollection<Category> Category;
    }

}
