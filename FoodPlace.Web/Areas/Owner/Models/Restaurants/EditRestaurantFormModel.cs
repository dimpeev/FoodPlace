namespace FoodPlace.Web.Areas.Owner.Models.Restaurants
{
    using FoodPlace.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditRestaurantFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.RestaurantNameMinLength)]
        [MaxLength(ModelConstants.RestaurantNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ModelConstants.RestaurantDescriptionMaxLength)]
        public string Description { get; set; }

        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();

        [Display(Name = "Menu")]
        public int MenuId { get; set; }

        public IEnumerable<SelectListItem> Menus { get; set; } = new List<SelectListItem>();
    }
}