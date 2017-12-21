namespace FoodPlace.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Home;
    using Services.Home;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var result = await this.homeService.AllAsync();

            var model = new HomeRestaurantListingViewModel
            {
                Restaurants = result.restaurants.Where(r => id == null ? r.CityId != -1 : r.CityId == id),
                Cities = result
                    .cities
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    })
                    .OrderBy(t => t.Text)
                    .ToList(),
                CityId = id
            };
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            return View(await this.homeService.RestaurantMenuAsync(id));
        }

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404 || statusCode == 500)
                {
                    var viewName = statusCode.ToString();
                    return View(viewName);
                }
            }
            return View();
        }
    }
}
