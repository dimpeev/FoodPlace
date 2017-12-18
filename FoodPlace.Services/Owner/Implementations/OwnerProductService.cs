namespace FoodPlace.Services.Owner.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models.Product;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OwnerProductService : IOwnerProductService
    {
        private readonly FoodPlaceDbContext db;

        public OwnerProductService(FoodPlaceDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<OwnerProductListingServiceModel>> MyProductsAsync(string ownerId)
            => await db
                .Products
                .Where(r => r.OwnerId == ownerId)
                .ProjectTo<OwnerProductListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalProductsAsync(string ownerId)
            => await db.Products.CountAsync(r => r.OwnerId == ownerId);
    }
}