namespace FoodPlace.Services.Home.Models
{
    using System.Collections.Generic;

    public class HomeIndexServiceModel
    {
        public IEnumerable<HomeRestaurantListingServiceModel> restaurants { get; set; }

        public IEnumerable<HomeCitiesListingServiceModel> cities { get; set; }
    }
}