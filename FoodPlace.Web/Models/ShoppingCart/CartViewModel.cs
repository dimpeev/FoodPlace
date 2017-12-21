namespace FoodPlace.Web.Models.ShoppingCart
{
    using System.Collections.Generic;

    public class CartViewModel
    {
        public int RestauratId { get; set; }

        public IEnumerable<CartItemsViewModel> Items { get; set; } = new List<CartItemsViewModel>();
    }
}