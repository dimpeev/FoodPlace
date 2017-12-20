namespace FoodPlace.Services.Owner.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using FoodPlace.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Menu;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OwnerMenuService : IOwnerMenuService
    {
        private readonly FoodPlaceDbContext db;

        public OwnerMenuService(FoodPlaceDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<OwnerMenuListingServiceModel>> MyMenusAsync(string ownerId)
            => await this.db
                .Menus
                .Where(r => r.OwnerId == ownerId)
                .ProjectTo<OwnerMenuListingServiceModel>()
                .ToListAsync();

        public async Task<int> TotalMenusAsync(string ownerId)
            => await this.db.Menus.CountAsync(r => r.OwnerId == ownerId);

        public async Task Create(string name, string ownerId)
        {
            var ownerExists = await this.db.Users.AnyAsync(u => u.Id == ownerId);
            if (!ownerExists)
            {
                return;
            }

            var menu = new Menu
            {
                Name = name,
                OwnerId = ownerId
            };

            await this.db.Menus.AddAsync(menu);
            await this.db.SaveChangesAsync();
        }

        public async Task<OwnerEditMenuServiceModel> ByIdAsync(int id)
            => await this.db
                .Menus
                .Where(m => m.Id == id)
                .ProjectTo<OwnerEditMenuServiceModel>()
                .FirstOrDefaultAsync();

        public async Task Edit(int id, string name, IList<int> selectedProducts, string ownerId)
        {
            var menu = await this.db.Menus.Include(m => m.Products).FirstOrDefaultAsync(m => m.Id == id && m.OwnerId == ownerId);

            if (menu == null)
            {
                return;
            }

            menu.Name = name;

            foreach (var productId in selectedProducts)
            {
                if (menu.Products.Any(p => p.ProductId == productId))
                {
                    continue;
                }
                menu.Products.Add(new MenuProduct
                {
                    ProductId = productId,
                    MenuId = id
                });
            }

            await this.db.SaveChangesAsync();
        }
    }
}