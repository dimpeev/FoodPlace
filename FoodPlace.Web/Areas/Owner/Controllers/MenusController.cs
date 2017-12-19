namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using FoodPlace.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Menus;
    using Services.Owner;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Services;

    public class MenusController : BaseOwnerController
    {
        private readonly UserManager<User> userManager;
        private readonly IOwnerMenuService ownerMenuService;
        private readonly IOwnerFoodService ownerFoodService;

        public MenusController(UserManager<User> userManager, IOwnerMenuService ownerMenuService, IOwnerFoodService ownerFoodService)
        {
            this.userManager = userManager;
            this.ownerMenuService = ownerMenuService;
            this.ownerFoodService = ownerFoodService;
        }

        public async Task<IActionResult> All()
        {
            var user = await userManager.GetUserAsync(User);

            return View(new MenusListingViewModel
            {
                Menus = await ownerMenuService.MyMenusAsync(user.Id),
                TotalMenus = await ownerMenuService.TotalMenusAsync(user.Id)
            });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddMenuFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.ownerMenuService.Create(model.Name, user.Id);

            this.TempData.AddSuccessMessage("Menu added successfully.");

            return RedirectToAction(nameof(MenusController.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var menu = await ownerMenuService.ByIdAsync(id);
            if (menu == null)
            {
                return NotFound();
            }
            var user = await userManager.GetUserAsync(User);

            if (menu.OwnerId != user.Id)
            {
                return Forbid();
            }

            return View(new EditMenuFormModel
            {
                Id = menu.Id,
                Name = menu.Name,
                Products = menu.Products,
                AvailableProducts = await GetProducts(menu.Products.Select(p => p.Id).ToList())
            });
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(EditMenuFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableProducts = await GetProducts(model.Products.Select(p => p.Id).ToList());
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.ownerMenuService.Edit(model.Id, model.Name,model.SelectedProducts, user.Id);

            this.TempData.AddSuccessMessage("Menu edited successfully.");

            return RedirectToAction(nameof(MenusController.All));
        } 

        private async Task<IEnumerable<SelectListItem>> GetProducts(ICollection<int> ids)
        {
            var user = await userManager.GetUserAsync(User);

            var products = await this.ownerFoodService.MyProductsAsync(user.Id);

            return products
                .Where(p => !ids.Contains(p.Id))
                .Select(p => new SelectListItem
                {
                    Text = string.Format(ServiceConstants.FoodPriceFormat, p.Name, p.Price),
                    Value = p.Id.ToString()
                })
                .OrderBy(p => p.Text)
                .ToList();
        }
    }
}