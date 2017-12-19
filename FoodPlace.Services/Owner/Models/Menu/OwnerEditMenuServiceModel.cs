namespace FoodPlace.Services.Owner.Models.Menu
{
    using AutoMapper;
    using Common.Mapping;
    using FoodPlace.Models;
    using System.Collections.Generic;

    public class OwnerEditMenuServiceModel : IMapFrom<Menu>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string OwnerId { get; set; }

        public ICollection<OwnerProductsInMenuServiceModel>  Products { get; set; } = new List<OwnerProductsInMenuServiceModel>();

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<MenuProduct, OwnerProductsInMenuServiceModel>()
                .ForMember(m => m.Id,
                    cfg => cfg.MapFrom(m => m.ProductId))
                .ForMember(m => m.Info,
                    cfg => cfg.MapFrom(p => string.Format(ServiceConstants.FoodPriceFormat, p.Product.Name, p.Product.Price)));
        }
    }
}