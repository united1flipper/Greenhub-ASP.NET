using PlantShop.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models
{
    public class NewPlant
    {
        [Display(Name = "Plant name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Display(Name = "Plant image URL")]
        [Required(ErrorMessage = "Image is required")]
        public string ImageURL { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
        [Display(Name = "Price ( $ )")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Select plant categories")]
        [Required(ErrorMessage = "At least one is required")]
        public List<int> CategoryIds{ get; set; }

    }

}
