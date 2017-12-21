namespace FoodPlace.Services.Home.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
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
    }
}