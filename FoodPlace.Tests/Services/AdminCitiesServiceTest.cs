namespace FoodPlace.Tests.Services
{
    using Data;
    using FluentAssertions;
    using FoodPlace.Services.Admin.Implementations;
    using FoodPlace.Services.Admin.Models.City;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminCitiesServiceTest
    {
        public AdminCitiesServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task CitiesServiceAllAsyncShould_ReturnCorrentResult()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            var result = await adminCityService.AllAsync();

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 3
                            && r.ElementAt(1).Id == 1
                            && r.ElementAt(2).Id == 2)
                .And.HaveCount(3);
        }

        [Fact]
        public async Task CitiesServiceCreateShould_FailToAddNewCityToTheDatabase()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            await adminCityService.Create("sofia");

            // Assert
            db.Cities.Should()
                .HaveCount(3)
                .And
                .Subject
                .Should()
                .Match(c =>
                    c.ElementAt(0).Name == firstCity.Name &&
                    c.ElementAt(1).Name == secondCity.Name &&
                    c.ElementAt(2).Name == thirdCity.Name
                );
        }

        [Fact]
        public async Task CitiesServiceCreateShould_AddNewCityToTheDatabase()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Name = "New York" };
            var secondCity = new City { Name = "Sofia" };
            var thirdCity = new City { Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            await adminCityService.Create("Varna");

            // Assert
            db.Cities
                .Should()
                .HaveCount(4)
                .And
                .Subject
                .Should()
                .Match(c =>
                    c.ElementAt(0).Name == firstCity.Name &&
                    c.ElementAt(1).Name == secondCity.Name &&
                    c.ElementAt(2).Name == thirdCity.Name &&
                    c.ElementAt(3).Name == "Varna"
                );
        }

        [Fact]
        public async Task CitiesServiceExistsByNameAsyncShould_ReturnTrue()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Name = "New York" };
            var secondCity = new City { Name = "Sofia" };
            var thirdCity = new City { Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            var result = await adminCityService.ExistsByNameAsync("nEW yOrK");

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task CitiesServiceExistsByNameAsyncShould_ReturnFalse()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Name = "New York" };
            var secondCity = new City { Name = "Sofia" };
            var thirdCity = new City { Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            var result = await adminCityService.ExistsByNameAsync("Varna");

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task CitiesServiceExistsByNameWithDifferentIdAsyncShould_ReturnTrue()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            var result = await adminCityService.ExistsByNameWithDifferentIdAsync(4, "Sofia");

            // Assert
            result.Should().Be(true);
        }

        [Fact]
        public async Task CitiesServiceExistsByNameWithDifferentIdAsyncShould_ReturnFalse()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            var result = await adminCityService.ExistsByNameWithDifferentIdAsync(2, "Sofia");

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task CitiesServiceExistsByIdAsyncShould_ReturnCorrectModel()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            var result = await adminCityService.ByIdAsync(2);

            // Assert
            result.Should().BeAssignableTo<AdminCityEditServiceModel>();
            result.Name.Should().Be("Sofia");
        }

        [Fact]
        public async Task CitiesServiceEditShould_NotChangeCityName()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            await adminCityService.Edit(1, "Sofia");

            // Assert
            db.Cities.FirstOrDefault(c => c.Id == 1).Name.Should().NotBe("Sofia");
        }

        [Fact]
        public async Task CitiesServiceEditShould_ChangeCityName()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstCity = new City { Id = 1, Name = "New York" };
            var secondCity = new City { Id = 2, Name = "Sofia" };
            var thirdCity = new City { Id = 3, Name = "Argentina" };

            db.AddRange(firstCity, secondCity, thirdCity);

            await db.SaveChangesAsync();

            var adminCityService = new AdminCityService(db);

            // Act
            await adminCityService.Edit(1, "Varna");

            // Assert
            db.Cities.FirstOrDefault(c => c.Id == 1).Name.Should().Be("Varna");
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