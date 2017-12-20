namespace FoodPlace.Web.Areas.Owner.Models.Food
{
    using Services.Owner.Models.Food;
    using System.Collections.Generic;

    public class FoodListingViewModel
    {
        public IEnumerable<OwnerProductListingServiceModel> Products { get; set; }

        public int TotalProducts { get; set; }
    }
}