namespace FoodPlace.Services.Home.Models
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class HomeCitiesListingServiceModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}