namespace FoodPlace.Services.Owner.Models.Menu
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerEditMenuServiceModel : IMapFrom<Menu>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }
    }
}