namespace FoodPlace.Services.ShoppingCart
{
    using Models;
    using System.Collections.Generic;

    public interface IShoppingCartService
    {
        void AddToCart(string id, int productId, int restaurantId);

        void RemoveFromCart(string id, int productId);

        void ClearCart(string id);

        IEnumerable<CartItem> GetItems(string id);

        int GetRestauratId (string id);
    }
}