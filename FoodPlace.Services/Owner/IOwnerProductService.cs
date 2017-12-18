namespace FoodPlace.Services.Owner
{
    using Models.Product;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOwnerProductService
    {
        Task<IEnumerable<OwnerProductListingServiceModel>> MyProductsAsync(string ownerId);

        Task<int> TotalProductsAsync(string ownerId);
    }
}