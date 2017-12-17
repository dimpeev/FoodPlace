namespace FoodPlace.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminCityService
    {
        Task<IEnumerable<AdminCityListingServiceModel>> AllAsync();

        Task Create(string name);

        Task<bool> ExistsByNameAsync(string name);

        Task<bool> ExistsByNameWithDifferentIdAsync(int id, string name);

        Task<AdminCityEditServiceModel> ByIdAsync(int id);

        Task Edit(int id, string name);
    }
}