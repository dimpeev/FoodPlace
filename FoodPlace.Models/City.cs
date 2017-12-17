namespace FoodPlace.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.CityNameMinLength)]
        [MaxLength(ModelConstants.CityNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
