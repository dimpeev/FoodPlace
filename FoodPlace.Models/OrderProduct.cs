namespace FoodPlace.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderProduct
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}