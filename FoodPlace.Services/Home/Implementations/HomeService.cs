namespace FoodPlace.Services.Home.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeService : IHomeService
    {
        private readonly FoodPlaceDbContext db;

        public HomeService(FoodPlaceDbContext db)
        {
            this.db = db;
        }

        public async Task<HomeIndexServiceModel> AllAsync()
        {
            var allRestaurants = await this.db.Restaurants.ProjectTo<HomeRestaurantListingServiceModel>().ToListAsync();
            var allCities = await this.db.Cities.ProjectTo<HomeCitiesListingServiceModel>().ToListAsync();

            return new HomeIndexServiceModel
            {
                cities = allCities,
                restaurants = allRestaurants
            };
        }

        public async Task<HomeRestaurantMenuListingServiceModel> RestaurantMenuAsync(int restaurantId)
        {
            var restaurant = await this.db.Restaurants.Include(r => r.Menu).FirstOrDefaultAsync(r => r.Id == restaurantId);
            if (restaurant?.Menu == null)
            {
                return new HomeRestaurantMenuListingServiceModel();
            }

            var productsInMenu = await this.db
                .Products
                .Where(p => p.Menus.Any(m => m.MenuId == restaurant.MenuId))
                .ProjectTo<ProductsInMenuListingServiceModel>()
                .ToListAsync();

            return(new HomeRestaurantMenuListingServiceModel
            {
                RestaurantId = restaurantId,
                Products = productsInMenu
            });
        }
    }
}