using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using AuthOnlineApp.Controllers;
using AuthOnlineApp.Data;
using AuthOnlineApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthOnlineApp.Tests
{
    public class HomeControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            context.Product.AddRange(new List<Product>
            {
                new Product
                {
                    Name = "Phone",
                    StartingPrice = 100,
                    Description = "Test phone",
                    ImageUrl = "test.jpg",
                    CreatedByUserId = "1"
                },
                new Product
                {
                    Name = "Laptop",
                    StartingPrice = 500,
                    Description = "Test laptop",
                    ImageUrl = "test.jpg",
                    CreatedByUserId = "1"
                }
            });

            context.SaveChanges();

            return context;
        }

        private HomeController GetController(ApplicationDbContext context)
        {
            var logger = new Mock<ILogger<HomeController>>();
            return new HomeController(logger.Object, context);
        }

        [Fact]
        public async Task Index_ShouldReturn_AllProducts()
        {
            var controller = GetController(GetDbContext());

            var result = await controller.Index(null, null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task Index_ShouldFilter_BySearchString()
        {
            var controller = GetController(GetDbContext());

            var result = await controller.Index("Phone", null, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Single(model);
            Assert.Equal("Phone", model.First().Name);
        }

        [Fact]
        public async Task Index_ShouldFilter_ByMinPrice()
        {
            var controller = GetController(GetDbContext());

            var result = await controller.Index(null, 200, null);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Single(model);
            Assert.Equal("Laptop", model.First().Name);
        }

        [Fact]
        public async Task Index_ShouldFilter_ByMaxPrice()
        {
            var controller = GetController(GetDbContext());

            var result = await controller.Index(null, null, 200);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Product>>(viewResult.Model);

            Assert.Single(model);
            Assert.Equal("Phone", model.First().Name);
        }

        [Fact]
        public void Privacy_ShouldReturnView()
        {
            var controller = GetController(GetDbContext());

            var result = controller.Privacy();

            Assert.IsType<ViewResult>(result);
        }
    }
}