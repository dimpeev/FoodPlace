namespace FoodPlace.Services.Owner
{
    using Models.Menu;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOwnerMenuService
    {
        Task<IEnumerable<OwnerMenuListingServiceModel>> MyMenusAsync(string ownerId);

        Task<int> TotalMenusAsync(string ownerId);

        Task Create(string name, string ownerId);

        Task<OwnerEditMenuServiceModel> ByIdAsync(int id);

        Task Edit(int id, string name, string ownerId);
    }
}