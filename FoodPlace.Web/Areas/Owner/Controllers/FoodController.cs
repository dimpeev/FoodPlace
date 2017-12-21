namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using FoodPlace.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Food;
    using Services.Owner;
    using System.Threading.Tasks;

    public class FoodController : BaseOwnerController
    {
        private readonly UserManager<User> userManager;
        private readonly IOwnerFoodService ownerFoodService;

        public FoodController(UserManager<User> userManager, IOwnerFoodService ownerFoodService)
        {
            this.userManager = userManager;
            this.ownerFoodService = ownerFoodService;
        }

        public async Task<IActionResult> All()
        {
            var user = await userManager.GetUserAsync(User);

            return View(new FoodListingViewModel
            {
                Products = await ownerFoodService.MyProductsAsync(user.Id),
                TotalProducts = await ownerFoodService.TotalProductsAsync(user.Id)
            });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFoodFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.ownerFoodService.Create(model.Name, model.Description, model.Price, user.Id);

            this.TempData.AddSuccessMessage("Product added successfully.");

            return RedirectToAction(nameof(FoodController.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var food = await ownerFoodService.ByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            var user = await userManager.GetUserAsync(User);

            if (food.OwnerId != user.Id)
            {
                return Forbid();
            }

            return View(new EditFoodFormModel
            {
                Id = food.Id,
                Name = food.Name,
                Description = food.Description,
                Price = food.Price
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditFoodFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.ownerFoodService.Edit(model.Id, model.Name, model.Description, model.Price, user.Id);

            this.TempData.AddSuccessMessage("Product edited successfully.");

            return RedirectToAction(nameof(FoodController.All));
        }
    }
}