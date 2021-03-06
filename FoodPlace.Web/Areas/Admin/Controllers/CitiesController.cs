﻿namespace FoodPlace.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Admin;
    using System.Threading.Tasks;

    public class CitiesController : BaseAdminController
    {
        private readonly IAdminCityService adminCityService;

        public CitiesController(IAdminCityService adminCityService)
        {
            this.adminCityService = adminCityService;
        }

        public async Task<IActionResult> All()
            => View(await adminCityService.AllAsync());

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCityFormModel model)
        {
            if (await adminCityService.ExistsByNameAsync(model.Name))
            {
                this.ModelState.AddModelError(string.Empty, "City with the same name already exists.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.adminCityService.Create(model.Name);

            this.TempData.AddSuccessMessage("City added successfully.");

            return RedirectToAction(nameof(CitiesController.All));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var city = await adminCityService.ByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            return View(new EditCityFormModel
            {
                Id = city.Id,
                Name = city.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCityFormModel model)
        {
            if (await adminCityService.ExistsByNameWithDifferentIdAsync(model.Id, model.Name))
            {
                this.ModelState.AddModelError(string.Empty, "City with the same name already exists.");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.adminCityService.Edit(model.Id, model.Name);

            this.TempData.AddSuccessMessage("City edited successfully.");

            return RedirectToAction(nameof(CitiesController.All));
        }
    }
}