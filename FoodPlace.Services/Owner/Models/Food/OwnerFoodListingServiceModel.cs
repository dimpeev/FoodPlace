namespace FoodPlace.Services.Owner.Models.Food
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerProductListingServiceModel : IMapFrom<Food>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}