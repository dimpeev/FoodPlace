namespace FoodPlace.Web.Areas.Admin.Models
{
    using FoodPlace.Models;
    using System.ComponentModel.DataAnnotations;

    public class AddCityFormModel
    {
        [Required]
        [MinLength(ModelConstants.CityNameMinLength)]
        [MaxLength(ModelConstants.CityNameMaxLength)]
        public string Name { get; set; }
    }
}