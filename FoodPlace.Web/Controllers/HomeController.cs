namespace FoodPlace.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Home;
    using Services.Home;

    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var result = await homeService.AllAsync();

            var model = new HomeRestaurantListingViewModel
            {
                Restaurants = result.restaurants.Where(r => id == null ? r.Id != -1 : r.Id == id),
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

        public IActionResult Details(int id)
        {
            return View();
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
