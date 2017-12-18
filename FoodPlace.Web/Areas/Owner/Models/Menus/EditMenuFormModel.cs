namespace FoodPlace.Web.Areas.Owner.Models.Menus
{
    using FoodPlace.Models;
    using System.ComponentModel.DataAnnotations;

    public class EditMenuFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MenuNameMinLength)]
        [MaxLength(ModelConstants.MenuNameMaxLength)]
        public string Name { get; set; }
    }
}