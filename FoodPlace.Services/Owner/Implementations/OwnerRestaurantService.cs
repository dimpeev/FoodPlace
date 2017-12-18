namespace FoodPlace.Services.Owner.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
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
            => await db
                .Restaurants
                .Where(r => r.OwnerId == ownerId)
                .ProjectTo<OwnerRestaurantListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalRestaurantsAsync(string ownerId)
            => await db.Restaurants.CountAsync(r => r.OwnerId == ownerId);
    }
}