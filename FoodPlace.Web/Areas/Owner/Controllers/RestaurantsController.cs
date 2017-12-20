namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using FoodPlace.Models;
    using Infrastructure.Extensions;
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
        private readonly IOwnerMenuService adminMenuService;

        public RestaurantsController(UserManager<User> userManager, IOwnerRestaurantService ownerRestaurantService, IAdminCityService adminCityService, IOwnerMenuService adminMenuService)
        {
            this.userManager = userManager;
            this.ownerRestaurantService = ownerRestaurantService;
            this.adminCityService = adminCityService;
            this.adminMenuService = adminMenuService;
        }

        public async Task<IActionResult> All()
        {
            var user = await userManager.GetUserAsync(User);
            return View(new RestaurantsListingViewModel
            {
                Restaurants = await ownerRestaurantService.MyRestaurantsAsync(user.Id),
                TotalRestaurants = await ownerRestaurantService.TotalRestaurantsAsync(user.Id)
            });
        }

        public async Task<IActionResult> Add()
        {
            var user = await userManager.GetUserAsync(User);
            return View(new AddRestaurantFormModel
            {
                Cities = await GetCities(),
                Menus = await GetMenus(user.Id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRestaurantFormModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                model.Cities = await GetCities();
                model.Menus = await GetMenus(user.Id);
                return View(model);
            }

            await this.ownerRestaurantService.Create(model.Name, model.Description, model.CityId, model.MenuId,
                user.Id);

            this.TempData.AddSuccessMessage("Restaurant added successfully.");

            return RedirectToAction(nameof(RestaurantsController.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var restaurant = await ownerRestaurantService.ByIdAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            var user = await userManager.GetUserAsync(User);

            if (restaurant.OwnerId != user.Id)
            {
                return Forbid();
            }

            return View(new EditRestaurantFormModel()
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Description = restaurant.Description,
                CityId = restaurant.CityId,
                MenuId = restaurant.MenuId,
                Cities = await GetCities(),
                Menus = await GetMenus(user.Id)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRestaurantFormModel model)
        {
            var user = await userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                model.Cities = await GetCities();
                model.Menus = await GetMenus(user.Id);
                return View(model);
            }

            await this.ownerRestaurantService.Edit(model.Id, model.Name, model.Description, model.CityId, model.MenuId, user.Id);

            this.TempData.AddSuccessMessage("Restaurant edited successfully.");

            return RedirectToAction(nameof(RestaurantsController.All));
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

        private async Task<IEnumerable<SelectListItem>> GetMenus(string ownerId)
        {
            var menus = await this.adminMenuService.MyMenusAsync(ownerId);
            return menus
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