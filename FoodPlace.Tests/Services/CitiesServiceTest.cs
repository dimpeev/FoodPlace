namespace FoodPlace.Tests.Services
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System;
    using System.Linq;
    using FluentAssertions;
    using FoodPlace.Services.Admin.Implementations;
    using Models;
    using Xunit;

    public class CitiesServiceTest
    {
        public CitiesServiceTest()
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
        public async Task CitiesServiceCreateShould_ReturnCorrentResult()
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
            db.Cities.Should().HaveCount(3);
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