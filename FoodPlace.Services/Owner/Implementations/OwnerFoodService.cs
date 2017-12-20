namespace FoodPlace.Services.Owner.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using FoodPlace.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Food;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OwnerFoodService : IOwnerFoodService
    {
        private readonly FoodPlaceDbContext db;

        public OwnerFoodService(FoodPlaceDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<OwnerProductListingServiceModel>> MyProductsAsync(string ownerId)
            => await this.db
                .Products
                .Where(r => r.OwnerId == ownerId)
                .ProjectTo<OwnerProductListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalProductsAsync(string ownerId)
            => await this.db.Products.CountAsync(r => r.OwnerId == ownerId);

        public async Task Create(string name, string description, decimal price, string ownerId)
        {
            var ownerExists = await this.db.Users.AnyAsync(u => u.Id == ownerId);
            if (!ownerExists)
            {
                return;
            }

            var food = new Food
            {
                Name = name,
                Description = description,
                Price = price,
                OwnerId = ownerId
            };

            await this.db.Products.AddAsync(food);
            await this.db.SaveChangesAsync();
        }

        public async Task<OwnerEditFoodServiceModel> ByIdAsync(int id)
            => await this.db
                .Products
                .Where(p => p.Id == id)
                .ProjectTo<OwnerEditFoodServiceModel>()
                .FirstOrDefaultAsync();

        public async Task Edit(int id, string name, string description, decimal price, string ownerId)
        {
            var food = await this.db.Products.FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId);

            if (food == null)
            {
                return;
            }

            food.Name = name;
            food.Description = description;
            food.Price = price;

            await this.db.SaveChangesAsync();
        }
    }
}