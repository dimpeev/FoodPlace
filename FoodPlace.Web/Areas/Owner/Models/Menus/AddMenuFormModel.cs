namespace FoodPlace.Web.Areas.Owner.Models.Menus
{
    using FoodPlace.Models;
    using System.ComponentModel.DataAnnotations;

    public class AddMenuFormModel
    {
        [Required]
        [MinLength(ModelConstants.MenuNameMinLength)]
        [MaxLength(ModelConstants.MenuNameMaxLength)]
        public string Name { get; set; }
    }
}