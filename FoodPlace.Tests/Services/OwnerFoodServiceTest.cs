namespace FoodPlace.Tests.Services
{
    using Data;
    using FluentAssertions;
    using FoodPlace.Services.Owner.Implementations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FoodPlace.Services.Owner.Models.Food;
    using Xunit;

    public class OwnerFoodServiceTest
    {
        public OwnerFoodServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task OwnerFoodServiceMyProductsAsyncShould_ReturnFoodAddedByOnlyOneOwner()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstFood = new Food { Name = "Banana", OwnerId = "owner1" };
            var secondFood = new Food { Name = "Apple", OwnerId = "owner2" };
            var thirdFood = new Food { Name = "Peach", OwnerId = "owner1" };

            db.AddRange(firstFood, secondFood, thirdFood);

            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            var result = await ownerFoodService.MyProductsAsync("owner1");

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Name == "Banana"
                            && r.ElementAt(1).Name == "Peach")
                .And.HaveCount(2);
        }

        [Fact]
        public async Task OwnerFoodServiceTotalProductsAsyncShould_ReturnFoodCountAddedByOneOwner()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstFood = new Food { Name = "Banana", OwnerId = "owner1" };
            var secondFood = new Food { Name = "Apple", OwnerId = "owner2" };
            var thirdFood = new Food { Name = "Peach", OwnerId = "owner1" };

            db.AddRange(firstFood, secondFood, thirdFood);

            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            var result = await ownerFoodService.TotalProductsAsync("owner2");

            // Assert
            result
                .Should()
                .Be(1);
        }

        [Fact]
        public async Task OwnerFoodServiceCreateShould_NotAddFoodToTheDatabase()
        {
            // Arrange
            var db = this.GetDatabase();
            db.Users.Add(new User
            {
                Id = "user123"
            });
            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            await ownerFoodService.Create("TestName", "TestDescription", 1m, "admin");

            // Assert
            db.Products.Count().Should().NotBe(1);
        }

        [Fact]
        public async Task OwnerFoodServiceCreateShould_AddFoodToTheDatabase()
        {
            // Arrange
            var db = this.GetDatabase();
            db.Users.Add(new User
            {
                Id = "user123"
            });
            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            await ownerFoodService.Create("TestName", "TestDescription", 1m, "user123");

            // Assert
            db.Products.Count().Should().Be(1);
        }

        [Fact]
        public async Task OwnerFoodServiceByIdAsyncShould_ReturnCorrectServiceModel()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstFood = new Food { Id = 1 };
            var secondFood = new Food { Id = 2 };
            var thirdFood = new Food { Id = 3 };

            db.AddRange(firstFood, secondFood, thirdFood);

            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            var result = await ownerFoodService.ByIdAsync(1);

            // Assert
            result.Should().BeAssignableTo<OwnerEditFoodServiceModel>();
            result.Id.Should().Be(1);
        }

        [Fact]
        public async Task OwnerFoodServiceEditShould_NotChangeFoodNameBecauseOfWrongOwner()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstFood = new Food { Id = 1, Name = "Banana", OwnerId = "owner1" };
            var secondFood = new Food { Id = 2, Name = "Apple", OwnerId = "owner2" };
            var thirdFood = new Food { Id = 3, Name = "Peach", OwnerId = "owner1" };

            db.AddRange(firstFood, secondFood, thirdFood);

            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            await ownerFoodService.Edit(1, "Orange", "", 1m, "owner2");

            // Assert
            db.Products.FirstOrDefault(f => f.Id == 1).Name.Should().Be("Banana");
        }

        [Fact]
        public async Task OwnerFoodServiceEditShould_NotChangeFoodNameBecauseOfWrongId()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstFood = new Food { Id = 1, Name = "Banana", OwnerId = "owner1" };
            var secondFood = new Food { Id = 2, Name = "Apple", OwnerId = "owner2" };
            var thirdFood = new Food { Id = 3, Name = "Peach", OwnerId = "owner1" };

            db.AddRange(firstFood, secondFood, thirdFood);

            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            await ownerFoodService.Edit(4, "Orange", "", 1m, "owner1");

            // Assert
            db.Products.FirstOrDefault(f => f.Id == 1).Name.Should().Be("Banana");
        }

        [Fact]
        public async Task OwnerFoodServiceEditShould_ChangeFoodName()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstFood = new Food { Id = 1, Name = "Banana", OwnerId = "owner1" };
            var secondFood = new Food { Id = 2, Name = "Apple", OwnerId = "owner2" };
            var thirdFood = new Food { Id = 3, Name = "Peach", OwnerId = "owner1" };

            db.AddRange(firstFood, secondFood, thirdFood);

            await db.SaveChangesAsync();

            var ownerFoodService = new OwnerFoodService(db);

            // Act
            await ownerFoodService.Edit(1, "Orange", "", 1m, "owner1");

            // Assert
            db.Products.FirstOrDefault(f => f.Id == 1).Name.Should().Be("Orange");
        }

        private FoodPlaceDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<FoodPlaceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new FoodPlaceDbContext(dbOptions);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}