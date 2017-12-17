namespace FoodPlace.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using FoodPlace.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class AdminCityService : IAdminCityService
    {
        private readonly FoodPlaceDbContext db;

        public AdminCityService(FoodPlaceDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminCityListingServiceModel>> AllAsync()
            => await this.db
                .Cities
                .OrderBy(c => c.Name)
                .ProjectTo<AdminCityListingServiceModel>()
                .ToListAsync();

        public async Task Create(string name)
        {
            if (await ExistsByNameAsync(name))
            {
                return;
            }

            var city = new City
            {
                Name = name
            };

            await this.db.Cities.AddAsync(city);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
            => await db
                .Cities
                .AnyAsync(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase));

        public async Task<bool> ExistsByNameWithDifferentIdAsync(int id, string name)
            => await db
                .Cities
                .AnyAsync(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase) && c.Id != id);

        public async Task<AdminCityEditServiceModel> ByIdAsync(int id)
            => await db
                .Cities
                .Where(c => c.Id == id)
                .ProjectTo<AdminCityEditServiceModel>()
                .FirstOrDefaultAsync();

        public async Task Edit(int id, string name)
        {
            if (await ExistsByNameWithDifferentIdAsync(id, name))
            {
                return;
            }

            var city = await db.Cities.FirstOrDefaultAsync(c => c.Id == id);
            city.Name = name;

            await this.db.SaveChangesAsync();
        }
    }
}