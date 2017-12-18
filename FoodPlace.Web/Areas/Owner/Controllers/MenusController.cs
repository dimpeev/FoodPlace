namespace FoodPlace.Web.Areas.Owner.Controllers
{
    using FoodPlace.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Menus;
    using Services.Owner;
    using System.Threading.Tasks;

    public class MenusController : BaseOwnerController
    {
        private readonly UserManager<User> userManager;
        private readonly IOwnerMenuService ownerMenuService;

        public MenusController(UserManager<User> userManager, IOwnerMenuService ownerMenuService)
        {
            this.userManager = userManager;
            this.ownerMenuService = ownerMenuService;
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
                Name = menu.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditMenuFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.ownerMenuService.Edit(model.Id, model.Name, user.Id);

            this.TempData.AddSuccessMessage("Menu edited successfully.");

            return RedirectToAction(nameof(MenusController.All));
        }
    }
}