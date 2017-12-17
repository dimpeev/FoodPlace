﻿namespace FoodPlace.Services.Admin.Models
{
    using Common.Mapping;
    using FoodPlace.Models;

    public class AdminCityEditServiceModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}