namespace FoodPlace.Services.Owner.Models.Restaurant
{
    using AutoMapper;
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerRestaurantListingServiceModel : IMapFrom<Restaurant>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int MenuId { get; set; }

        public string MenuName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<City, OwnerRestaurantListingServiceModel>()
                .ForMember(r => r.CityName,
                    cfg => cfg.MapFrom(c => c.Name));

            mapper
                .CreateMap<Menu, OwnerRestaurantListingServiceModel>()
                .ForMember(r => r.MenuName,
                    cfg => cfg.MapFrom(m => m.Name));
        }
    }
}