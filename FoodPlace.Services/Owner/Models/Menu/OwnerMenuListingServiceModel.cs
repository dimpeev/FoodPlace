namespace FoodPlace.Services.Owner.Models.Menu
{
    using AutoMapper;
    using Common.Mapping;
    using FoodPlace.Models;

    public class OwnerMenuListingServiceModel : IMapFrom<Menu>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfPrductsInMenu { get; set; }
        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Menu, OwnerMenuListingServiceModel>()
                .ForMember(m => m.NumberOfPrductsInMenu,
                    cfg => cfg.MapFrom(m => m.Products.Count));
        }
    }
}