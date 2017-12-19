namespace FoodPlace.Web.Areas.Owner.Models.Menus
{
    using FoodPlace.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Owner.Models.Menu;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditMenuFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MenuNameMinLength)]
        [MaxLength(ModelConstants.MenuNameMaxLength)]
        public string Name { get; set; }

        public ICollection<OwnerProductsInMenuServiceModel> Products { get; set; }

        public IEnumerable<SelectListItem> AvailableProducts { get; set; } = new List<SelectListItem>();

        [Display(Name="Available products")]
        public List<int> SelectedProducts { get; set; } = new List<int>();
    }
}