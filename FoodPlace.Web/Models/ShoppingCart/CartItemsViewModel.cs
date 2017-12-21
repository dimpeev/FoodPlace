namespace FoodPlace.Web.Models.ShoppingCart
{
    public class CartItemsViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }
    }
}