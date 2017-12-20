namespace FoodPlace.Services.Owner.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using FoodPlace.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Restaurant;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OwnerRestaurantService : IOwnerRestaurantService 
    {
        private readonly FoodPlaceDbContext db;

        public OwnerRestaurantService(FoodPlaceDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<OwnerRestaurantListingServiceModel>> MyRestaurantsAsync(string ownerId)
            => await this.db
                .Restaurants
                .Where(r => r.OwnerId == ownerId)
                .ProjectTo<OwnerRestaurantListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalRestaurantsAsync(string ownerId)
            => await this.db.Restaurants.CountAsync(r => r.OwnerId == ownerId);

        public async Task Create(string name, string description, int cityId, int menuId, string ownerId)
        {
            var ownerExists = await this.db.Users.AnyAsync(u => u.Id == ownerId);
            if (!ownerExists)
            {
                return;
            }

            var restaurant = new Restaurant
            {
                Name = name,
                Description = description,
                CityId = cityId,
                MenuId = menuId,
                OwnerId = ownerId
            };

            await this.db.Restaurants.AddAsync(restaurant);
            await this.db.SaveChangesAsync();
        }

        public async Task<OwnerEditRestaurantServiceModel> ByIdAsync(int id)
            => await this.db
                .Restaurants
                .Where(m => m.Id == id)
                .ProjectTo<OwnerEditRestaurantServiceModel>()
                .FirstOrDefaultAsync();

        public async Task Edit(int id, string name, string description, int cityId, int menuId, string userId)
        {
            var restaurant = await this.db.Restaurants.FirstOrDefaultAsync(m => m.Id == id && m.OwnerId == userId);

            if (restaurant == null)
            {
                return;
            }

            restaurant.Name = name;
            restaurant.Description = description;
            restaurant.CityId = cityId;
            restaurant.MenuId = menuId;

            await db.SaveChangesAsync();
        }
    }
}