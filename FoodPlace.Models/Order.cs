namespace FoodPlace.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public ICollection<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        public string ClientId { get; set; }

        public User Client { get; set; }
    }
}