namespace FoodPlace.Services.Home.Models
{
    using Common.Mapping;
    using FoodPlace.Models;
    using System.Collections.Generic;

    public class HomeRestaurantMenuListingServiceModel : IMapFrom<Menu>
    {
        public string Name { get; set; }

        public int RestaurantId { get; set; }

        public ICollection<ProductsInMenuListingServiceModel> Products { get; set; }
    }
}