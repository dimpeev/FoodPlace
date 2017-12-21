namespace FoodPlace.Services.Home.Models
{
    using AutoMapper;
    using Common.Mapping;
    using FoodPlace.Models;

    public class HomeRestaurantListingServiceModel : IMapFrom<Restaurant>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Restaurant, HomeRestaurantListingServiceModel>()
                .ForMember(r => r.CityName,
                    cfg => cfg.MapFrom(r => r.City.Name));
        }
    }
}