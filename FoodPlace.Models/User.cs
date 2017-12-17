namespace FoodPlace.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public ICollection<Menu> Menus { get; set; } = new List<Menu>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
