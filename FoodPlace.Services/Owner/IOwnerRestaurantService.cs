namespace FoodPlace.Services.Owner
{
    using Models.Restaurant;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOwnerRestaurantService
    {
        Task<IEnumerable<OwnerRestaurantListingServiceModel>> MyRestaurantsAsync(string ownerId);

        Task<int> TotalRestaurantsAsync(string ownerId);

        Task Create(string name, string description, int cityId, int menuId, string ownerId);

        Task<OwnerEditRestaurantServiceModel> ByIdAsync(int id);

        Task Edit(int id, string name, string description, int cityId, int menuId, string userId);
    }
}