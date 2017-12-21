namespace FoodPlace.Services.ShoppingCart.Implementations
{
    using Models;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class ShoppingCartService : IShoppingCartService, IServiceSingleton
    {
        private readonly ConcurrentDictionary<string, ShoppingCart> carts;

        public ShoppingCartService()
        {
            this.carts = new ConcurrentDictionary<string, ShoppingCart>();
        }

        public void AddToCart(string id, int productId, int restaurantId)
        {
            var shoppingCart = GetShoppingCart(id);
            if(shoppingCart.RestaurantId != restaurantId)
            {
                shoppingCart.RestaurantId = restaurantId;
                shoppingCart.ClearCart();
            }

            shoppingCart.AddToCart(productId);
        }

        public void RemoveFromCart(string id, int productId)
        {
            var shoppingCart = GetShoppingCart(id);

            shoppingCart.RemoveFromCart(productId);
        }

        public void ClearCart(string id)
        {
            var shoppingCart = GetShoppingCart(id);

            shoppingCart.ClearCart();
        }

        public IEnumerable<CartItem> GetItems(string id)
        {
            var shoppingCart = GetShoppingCart(id);
            return new List<CartItem>(shoppingCart.Items);
        }

        public int GetRestauratId(string id)
        {
            var shoppingCart = GetShoppingCart(id);
            return shoppingCart.RestaurantId.GetValueOrDefault();
        }

        private ShoppingCart GetShoppingCart(string id)
            => this.carts.GetOrAdd(id, new ShoppingCart());
    }
}