namespace FoodPlace.Services.Owner.Models.Food
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerEditFoodServiceModel : IMapFrom<Food>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string OwnerId { get; set; }
    }
}