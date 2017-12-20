namespace FoodPlace.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Food
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.ProductNameMinLength)]
        [MaxLength(ModelConstants.ProductNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ModelConstants.ProductDescriptionNameMaxLength)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public ICollection<MenuProduct> Menus { get; set; } = new List<MenuProduct>();

        public ICollection<OrderProduct> Orders { get; set; } = new List<OrderProduct>();

        public string OwnerId { get; set; }

        public User Owner { get; set; }
    }
}