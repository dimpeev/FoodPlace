namespace FoodPlace.Services.Home.Models
{
    using System.Linq;
    using AutoMapper;
    using Common.Mapping;
    using FoodPlace.Models;

    public class HomeMyOrdersListing : IMapFrom<Order>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string RestaurantName { get; set; }

        public string City { get; set; }

        public int ProductsOrdered { get; set; }

        public decimal OrderPrice { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper
                .CreateMap<Order, HomeMyOrdersListing>()
                .ForMember(o => o.RestaurantName,
                    cfg => cfg.MapFrom(o => o.Restaurant.Name))
                .ForMember(o => o.City,
                    cfg => cfg.MapFrom(o => o.Restaurant.City.Name))
                .ForMember(o => o.ProductsOrdered,
                    cfg => cfg.MapFrom(o => o.Products.Count))
                .ForMember(o => o.OrderPrice,
                    cfg => cfg.MapFrom(o => o.Products.Sum(p => p.Price * p.Quantity)));
        }
    }
}