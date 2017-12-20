namespace FoodPlace.Services.Owner
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Food;

    public interface IOwnerFoodService
    {
        Task<IEnumerable<OwnerProductListingServiceModel>> MyProductsAsync(string ownerId);

        Task<int> TotalProductsAsync(string ownerId);

        Task Create(string name, string description, decimal price, string ownerId);

        Task<OwnerEditFoodServiceModel> ByIdAsync(int id);

        Task Edit(int id, string name, string description, decimal price, string ownerId);
    }
}