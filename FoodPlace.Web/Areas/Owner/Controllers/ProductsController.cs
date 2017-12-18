namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using FoodPlace.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Products;
    using Services.Owner;
    using System.Threading.Tasks;

    public class ProductsController : BaseOwnerController
    {
        private readonly UserManager<User> userManager;
        private readonly IOwnerProductService ownerProductService;

        public ProductsController(UserManager<User> userManager, IOwnerProductService ownerProductService)
        {
            this.userManager = userManager;
            this.ownerProductService = ownerProductService;
        }

        public async Task<IActionResult> All()
        {
            var ownerId = userManager.GetUserId(User);

            return View(new ProductsListingViewModel
            {
                Products = await ownerProductService.MyProductsAsync(ownerId),
                TotalProducts = await ownerProductService.TotalProductsAsync(ownerId)
            });
        }
    }
}