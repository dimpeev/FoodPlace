namespace FoodPlace.Services.Home.Models
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class ProductsInMenuListingServiceModel : IMapFrom<Food>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}