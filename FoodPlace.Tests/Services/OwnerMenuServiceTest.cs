namespace FoodPlace.Tests.Services
{
    using Data;
    using FluentAssertions;
    using FoodPlace.Services.Owner.Implementations;
    using FoodPlace.Services.Owner.Models.Menu;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OwnerMenuServiceTest
    {
        public OwnerMenuServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task OwnerMenuServiceMyMenusAsyncShould_ReturnMenusAddedByOwner()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstMenu = new Menu { Name = "Menu1", OwnerId = "owner1" };
            var secondMenu = new Menu { Name = "Menu2", OwnerId = "owner2" };
            var thirdMenu = new Menu { Name = "Menu3", OwnerId = "owner1" };

            db.AddRange(firstMenu, secondMenu, thirdMenu);

            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            var result = await ownerMenuService.MyMenusAsync("owner1");

            // Assert
            result
                .Should()
                .BeAssignableTo<IEnumerable<OwnerMenuListingServiceModel>>();
            result
                .Should()
                .Match(r => r.ElementAt(0).Name == "Menu1"
                            && r.ElementAt(1).Name == "Menu3")
                .And.HaveCount(2);
        }

        [Fact]
        public async Task OwnerMenuServiceTotalMenusAsyncShould_ReturnMenusAddedByOneOwnerCount()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstMenu = new Menu { Name = "Menu1", OwnerId = "owner1" };
            var secondMenu = new Menu { Name = "Menu2", OwnerId = "owner2" };
            var thirdMenu = new Menu { Name = "Menu3", OwnerId = "owner1" };

            db.AddRange(firstMenu, secondMenu, thirdMenu);

            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            var result = await ownerMenuService.TotalMenusAsync("owner1");

            // Assert
            result.Should().Be(2);
        }

        [Fact]
        public async Task OwnerMenuServiceCreateShould_ReturnNotAddMenuToTheDatabase()
        {
            // Arrange
            var db = this.GetDatabase();

            db.Users.Add(new User
            {
                Id = "user123"
            });
            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            await ownerMenuService.Create("Menu1", "user");

            // Assert
            db.Menus.Count().Should().Be(0);
        }

        [Fact]
        public async Task OwnerMenuServiceCreateShould_ReturnAddMenuToTheDatabase()
        {
            // Arrange
            var db = this.GetDatabase();

            db.Users.Add(new User
            {
                Id = "user123"
            });
            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            await ownerMenuService.Create("Menu1", "user123");

            // Assert
            db.Menus.Count().Should().Be(1);
        }

        [Fact]
        public async Task OwnerMenuServiceByIdAsyncShould_ReturnCorrectServiceModel()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstMenu = new Menu { Id = 1 };
            var secondMenu = new Menu { Id = 2 };
            var thirdMenu = new Menu { Id = 3 };

            db.AddRange(firstMenu, secondMenu, thirdMenu);

            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            var result = await ownerMenuService.ByIdAsync(1);

            // Assert
            result.Should().BeAssignableTo<OwnerEditMenuServiceModel>();
            result.Id.Should().Be(1);
        }

        [Fact]
        public async Task OwnerMenuServiceEditShould_NotChangeMenuBecauseOfWrongOwner()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstMenu = new Menu { Id = 1, Name = "Menu1", OwnerId = "owner1" };
            var secondMenu = new Menu { Id = 2, Name = "Menu2", OwnerId = "owner2" };
            var thirdMenu = new Menu { Id = 3, Name = "Menu3", OwnerId = "owner1" };

            db.AddRange(firstMenu, secondMenu, thirdMenu);

            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            await ownerMenuService.Edit(1, "Menu3", new List<int>{1, 2 ,3}, "owner2");

            // Assert
            db.Menus.FirstOrDefault(m => m.Id == 1).Name.Should().Be("Menu1");
            db.Menus.FirstOrDefault(m => m.Id == 1).Products.Count.Should().Be(0);
        }

        [Fact]
        public async Task OwnerMenuServiceEditShould_ChangeMenu()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstMenu = new Menu { Id = 1, Name = "Menu1", OwnerId = "owner1" };
            var secondMenu = new Menu { Id = 2, Name = "Menu2", OwnerId = "owner2" };
            var thirdMenu = new Menu { Id = 3, Name = "Menu3", OwnerId = "owner1" };

            db.AddRange(firstMenu, secondMenu, thirdMenu);

            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            await ownerMenuService.Edit(1, "Menu256", new List<int> { 1, 2, 3 }, "owner1");

            // Assert
            db.Menus.FirstOrDefault(m => m.Id == 1).Name.Should().Be("Menu256");
            db.Menus.FirstOrDefault(m => m.Id == 1).Products.Count.Should().Be(3);
        }

        [Fact]
        public async Task OwnerMenuServiceEditShould_ChangeMenuNumberOfProducts()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstMenu = new Menu { Id = 1, Name = "Menu1", OwnerId = "owner1" };
            var secondMenu = new Menu { Id = 2, Name = "Menu2", OwnerId = "owner2" };
            var thirdMenu = new Menu { Id = 3, Name = "Menu3", OwnerId = "owner1" };

            secondMenu.Products.Add(new MenuProduct
            {
                ProductId = 1,
                MenuId = 2
            });

            secondMenu.Products.Add(new MenuProduct
            {
                ProductId = 2,
                MenuId = 2
            });

            db.AddRange(firstMenu, secondMenu, thirdMenu);

            await db.SaveChangesAsync();

            var ownerMenuService = new OwnerMenuService(db);

            // Act
            await ownerMenuService.Edit(2, "Menu2", new List<int> { 1, 2, 3, 1 }, "owner2");

            // Assert
            db.Menus.FirstOrDefault(m => m.Id == 2).Name.Should().Be("Menu2");
            db.Menus.FirstOrDefault(m => m.Id == 2).Products.Count.Should().Be(3);
        }

        private FoodPlaceDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<FoodPlaceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FoodPlaceDbContext(dbOptions);
        }
    }
}