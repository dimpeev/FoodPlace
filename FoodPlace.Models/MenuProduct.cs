namespace FoodPlace.Models
{
    public class MenuProduct
    {
        public int MenuId { get; set; }

        public Menu  Menu { get; set; }

        public int ProductId { get; set; }

        public Food Product { get; set; }
    }
}