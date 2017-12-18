namespace FoodPlace.Web.Areas.Owner.Models.Products
{
    using Services.Owner.Models.Product;
    using System.Collections.Generic;

    public class ProductsListingViewModel
    {
        public IEnumerable<OwnerProductListingServiceModel> Products { get; set; }

        public int TotalProducts { get; set; }
    }
}