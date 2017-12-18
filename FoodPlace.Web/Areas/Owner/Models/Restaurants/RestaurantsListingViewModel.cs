namespace FoodPlace.Web.Areas.Owner.Models.Restaurants
{
    using Services.Owner.Models.Restaurant;
    using System.Collections.Generic;

    public class RestaurantsListingViewModel
    {
        public IEnumerable<OwnerRestaurantListingServiceModel> Restaurants { get; set; }

        public int TotalRestaurants { get; set; }
    }
}