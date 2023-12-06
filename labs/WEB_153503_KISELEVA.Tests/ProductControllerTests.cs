using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_153503_KISELEVA.Controllers;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;
using WEB_153503_KISELEVA.Services.CategoryService;
using WEB_153503_KISELEVA.Services.ProductService;
using WEB_153503_KISELEVA.Extensions;
using System.Linq;

namespace WEB_153503_KISELEVA.Tests
{
	public class ProductControllerTests
	{
        [Fact]
        public async Task Index_ReturnsNotFound_WhenCategoriesNotReceived()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(s => s.GetCategoryListAsync())
                .ReturnsAsync(new ResponseData<List<Category>> { Success = false, ErrorMessage = "Ошибка при получении списка категорий" });

            var productServiceMock = new Mock<IProductService>();

            var controller = new ProductController(productServiceMock.Object, categoryServiceMock.Object);

            var result = await controller.Index(null, 1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Index_ReturnsNotFound_WhenProductsNotReceived()
        {
            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(s => s.GetCategoryListAsync())
               .ReturnsAsync(new ResponseData<List<Category>> { Success = true, Data = new List<Category>()});

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(s => s.GetProductListAsync(null, 1))
                .ReturnsAsync(new ResponseData<ProductListModel<Product>> { Success = false, ErrorMessage = "Ошибка при получении списка продуктов" });


            var controller = new ProductController(productServiceMock.Object, categoryServiceMock.Object);

            var result = await controller.Index(null, 1);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Index_CorrectAction()
        {

            var expectedCategories = new List<Category>() {
                    new Category { Id = 1, Name = "Name", NormalizedName = "name" }
                };

            var expectedCurrentCategory = "Все";

            var categoryServiceMock = new Mock<ICategoryService>();
            categoryServiceMock.Setup(s => s.GetCategoryListAsync())
               .ReturnsAsync(new ResponseData<List<Category>> { Data = expectedCategories });

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(s => s.GetProductListAsync(null, 1))
                .ReturnsAsync(new ResponseData<ProductListModel<Product>> { Data = new ProductListModel<Product>() { Items = new List<Product>() { new Product{ Id = 1, Name = "Product", Description="Description", CategoryNormalizedName = "name", Price = 10} } } });

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Request.Headers)
                .Returns(new HeaderDictionary { { "X-Requested-With", "XMLHttpRequest" } });

            var controllerContext = new ControllerContext
            {
                HttpContext = httpContextMock.Object
            };


            var controller = new ProductController(productServiceMock.Object, categoryServiceMock.Object)
            {
                ControllerContext = controllerContext
            };

            var result = await controller.Index(null, 1);

            Assert.True(
                expectedCategories.SequenceEqual(controller.ViewData["categories"] as List<Category>)
               );
            Assert.Equal(expectedCurrentCategory, controller.ViewData["currentCategory"]);
            Assert.IsType<ProductListModel<Product>>(controller.ViewData.Model);
        }
    }
}

