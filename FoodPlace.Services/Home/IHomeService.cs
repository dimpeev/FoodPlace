namespace FoodPlace.Services.Home
{
    using Models;
    using System.Threading.Tasks;

    public interface IHomeService
    {
        Task<HomeIndexServiceModel> AllAsync();

        Task<HomeRestaurantMenuListingServiceModel> RestaurantMenuAsync(int restaurantId);
    }
}