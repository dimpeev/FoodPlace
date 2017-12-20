namespace FoodPlace.Services.Owner.Models.Restaurant
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerEditRestaurantServiceModel : IMapFrom<Restaurant>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public int MenuId { get; set; }

        public string OwnerId { get; set; }
    }
}