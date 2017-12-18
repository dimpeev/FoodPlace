namespace FoodPlace.Services.Admin.Models.City
{
    using AutoMapper;
    using Common.Mapping;
    using FoodPlace.Models;

    public class AdminCityListingServiceModel : IMapFrom<City>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfRestaurants { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<City, AdminCityListingServiceModel>()
                .ForMember(c => c.NumberOfRestaurants,
                    cfg => cfg.MapFrom(c => c.Restaurants.Count));
        }
    }
}