namespace FoodPlace.Web.Areas.Owner.Models.Food
{
    using FoodPlace.Models;
    using System.ComponentModel.DataAnnotations;

    public class EditFoodFormModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ModelConstants.ProductNameMinLength)]
        [MaxLength(ModelConstants.ProductNameMaxLength)]
        public string Name { get; set; }

        [MaxLength(ModelConstants.ProductDescriptionNameMaxLength)]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string OwnerId { get; set; }

    }
}