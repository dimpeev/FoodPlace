namespace FoodPlace.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Menu
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.MenuNameMinLength)]
        [MaxLength(ModelConstants.MenuNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public ICollection<MenuProduct> Products { get; set; } = new List<MenuProduct>();

        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}