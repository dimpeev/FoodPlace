namespace FoodPlace.Services.Owner.Models.Product
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerProductListingServiceModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}