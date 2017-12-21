namespace FoodPlace.Web.Models.Home
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Home.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class HomeRestaurantListingViewModel
    {
        public IEnumerable<HomeRestaurantListingServiceModel> Restaurants { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}