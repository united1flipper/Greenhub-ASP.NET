using PlantShop.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models
{
    public class Category : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Icon")]
        [Required(ErrorMessage = "The icon is required")]
        public string Icon { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "The name field is required")]
        public string Description { get; set; }

        //RELATIONS
        public List<Plant_Category> Plants_Categories { get; set; }

        //???

        //public ICollection<Plant> Plant;
    }
}
