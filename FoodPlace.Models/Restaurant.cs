namespace FoodPlace.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.RestaurantNameMinLength)]
        [MaxLength(ModelConstants.RestaurantNameMaxLength)]
        public string Name { get; set; }

        [MinLength(ModelConstants.RestaurantDescriptionMaxLength)]
        public string Description { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}