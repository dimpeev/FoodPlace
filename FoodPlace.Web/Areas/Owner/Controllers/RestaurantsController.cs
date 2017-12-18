namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using FoodPlace.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Restaurants;
    using Services.Admin;
    using Services.Owner;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RestaurantsController : BaseOwnerController
    {
        private readonly UserManager<User> userManager;
        private readonly IOwnerRestaurantService ownerRestaurantService;
        private readonly IAdminCityService adminCityService;

        public RestaurantsController(UserManager<User> userManager, IOwnerRestaurantService ownerRestaurantService, IAdminCityService adminCityService)
        {
            this.userManager = userManager;
            this.ownerRestaurantService = ownerRestaurantService;
            this.adminCityService = adminCityService;
        }

        public async Task<IActionResult> All()
        {
            var ownerId = userManager.GetUserId(User);
            return View(new RestaurantsListingViewModel
            {
                Restaurants = await ownerRestaurantService.MyRestaurantsAsync(ownerId),
                TotalRestaurants = await ownerRestaurantService.TotalRestaurantsAsync(ownerId)
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetCities()
        {
            var cities = await this.adminCityService.AllAsync();
            return cities
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .OrderBy(t => t.Text)
                .ToList();
        }
    }
}