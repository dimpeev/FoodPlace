namespace FoodPlace.Services.Owner
{
    using Models.Restaurant;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOwnerRestaurantService
    {
        Task<IEnumerable<OwnerRestaurantListingServiceModel>> MyRestaurantsAsync(string ownerId);

        Task<int> TotalRestaurantsAsync(string ownerId);
    }
}