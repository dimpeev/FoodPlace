namespace FoodPlace.Web.Models.Home
{
    using Services.Home.Models;
    using System.Collections.Generic;

    public class HomeMyOrdersViewModel
    {
        public IEnumerable<HomeMyOrdersListing> MyOrders { get; set; }
    }
}