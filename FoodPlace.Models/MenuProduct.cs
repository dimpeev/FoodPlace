namespace FoodPlace.Models
{
    public class MenuProduct
    {
        public int MenuId { get; set; }

        public Menu  Menu { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}