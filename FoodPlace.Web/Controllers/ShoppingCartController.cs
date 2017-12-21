namespace FoodPlace.Web.Controllers
{
    using Data;
    using FoodPlace.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.ShoppingCart;
    using Services.ShoppingCart;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly UserManager<User> userManager;
        private readonly FoodPlaceDbContext db;

        public ShoppingCartController(IShoppingCartService shoppingCartService, UserManager<User> userManager, FoodPlaceDbContext db)
        {
            this.shoppingCartService = shoppingCartService;
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IActionResult> Items()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();
            var itemsWithDetails = await GetItemsFromCartWithQuantities(shoppingCartId);

            var cart = new CartViewModel
            {
                Items = itemsWithDetails,
                RestauratId = shoppingCartService.GetRestauratId(shoppingCartId)
            };

            return View(cart);
        }

        public IActionResult AddToCart(int? id, int? restaurantId)
        {
            if (restaurantId != null || id != null)
            {
                var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

                this.shoppingCartService.AddToCart(shoppingCartId, id.GetValueOrDefault(), restaurantId.GetValueOrDefault());
            }

            return RedirectToAction(nameof(ShoppingCartController.Items));
        }

        public IActionResult RemoveFromCart(int? id)
        {
            if (id != null)
            {
                var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

                this.shoppingCartService.RemoveFromCart(shoppingCartId, id.GetValueOrDefault());
            }

            return RedirectToAction(nameof(ShoppingCartController.Items));
        }

        [HttpPost]
        public async Task<IActionResult> FinishOrder()
        {
            var shoppingCartId = this.HttpContext.Session.GetShoppingCartId();

            var user = await userManager.GetUserAsync(User);

            var order = new Order
            {
                ClientId = user.Id,
                RestaurantId = this.shoppingCartService.GetRestauratId(shoppingCartId)
            };

            var itemsWithDetails = await GetItemsFromCartWithQuantities(shoppingCartId);

            foreach (var item in itemsWithDetails)
            {
                order.Products.Add(new OrderProduct
                {
                    ProductId = item.ProductId,
                    Price = item.ProductPrice,
                    Quantity = item.ProductQuantity
                });
            }

            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();

            this.shoppingCartService.ClearCart(shoppingCartId);

            this.TempData.AddSuccessMessage("Order accepted.");

            return Redirect("/");
        }

        private async Task<IList<CartItemsViewModel>> GetItemsFromCartWithQuantities(string shoppingCartId)
        {
            var shoppingCart = this.shoppingCartService.GetItems(shoppingCartId);

            var itemIds = shoppingCart.Select(i => i.ProductId);

            var itemQuantities = shoppingCart.ToDictionary(i => i.ProductId, i => i.Quantity);

            var itemsWithDetails = await this.db
                .Products
                .Where(p => itemIds.Contains(p.Id))
                .Select(p => new CartItemsViewModel
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    ProductPrice = p.Price
                }).ToListAsync();

            itemsWithDetails.ForEach(i => i.ProductQuantity = itemQuantities[i.ProductId]);

           return itemsWithDetails;
        }
    }
}