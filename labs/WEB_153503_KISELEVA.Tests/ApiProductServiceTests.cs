using System.Data.Common;
using Microsoft.Data.Sqlite;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.API.Data;
using WEB_153503_KISELEVA.API.Services;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;
using WEB_153503_KISELEVA.Services.ProductService;

namespace WEB_153503_KISELEVA.Tests
{
    public class ApiProductServiceTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<AppDbContext> _contextOptions;

        public ApiProductServiceTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            using var context = new AppDbContext(_contextOptions);

            if (context.Database.EnsureCreated())
            {
                using var viewCommand = context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
                CREATE VIEW AllResources AS
                SELECT Url
                FROM Blogs;";
                viewCommand.ExecuteNonQuery();
            }
            context.Products.AddRange(
                new Product { Name = "Product 1", Description = "Description 1", CategoryNormalizedName = "category1", Price = 10 },
                new Product { Name = "Product 2", Description = "Description 2", CategoryNormalizedName = "category1", Price = 20 },
                new Product { Name = "Product 3", Description = "Description 3", CategoryNormalizedName = "category2", Price = 30 },
                new Product { Name = "Product 4", Description = "Description 4", CategoryNormalizedName = "category2", Price = 40 },
                new Product { Name = "Product 5", Description = "Description 5", CategoryNormalizedName = "category3", Price = 50 },
                new Product { Name = "Product 6", Description = "Description 6", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 7", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 8", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 9", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 10", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 11", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 12", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 13", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 14", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 15", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 16", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 17", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 18", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 19", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 20", CategoryNormalizedName = "category3", Price = 60 },
                new Product { Name = "Product 6", Description = "Description 21", CategoryNormalizedName = "category3", Price = 60 }
            );
            context.SaveChanges();
        }

        AppDbContext CreateContext() => new AppDbContext(_contextOptions);

        public void Dispose() => _connection.Dispose();

        [Fact]
        public void ServiceReturnsFirstPageOfThreeItems()
        {
            using var context = CreateContext();
            var service = new ProductService(context, null, null);
            var result = service.GetProductListAsync(null).Result;
            Assert.IsType<ResponseData<ProductListModel<Product>>>(result);
            Assert.True(result.Success);
            Assert.Equal(1, result.Data.CurrentPage);
            Assert.Equal(3, result.Data.Items.Count);
            Assert.Equal(7, result.Data.TotalPages);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(0), result.Data.Items[0]);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(1), result.Data.Items[1]);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(2), result.Data.Items[2]);
        }

        [Fact]
        public void ServiceReturnsRightPageOfThreeItems()
        {
            using var context = CreateContext();
            var service = new ProductService(context, null, null);
            var result = service.GetProductListAsync(null, 2).Result;
            Assert.IsType<ResponseData<ProductListModel<Product>>>(result);
            Assert.True(result.Success);
            Assert.Equal(2, result.Data.CurrentPage);
            Assert.Equal(3, result.Data.Items.Count);
            Assert.Equal(7, result.Data.TotalPages);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(3), result.Data.Items[0]);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(4), result.Data.Items[1]);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(5), result.Data.Items[2]);
        }

        [Fact]
        public void ServiceFiltersByCategory()
        {
            using var context = CreateContext();
            var service = new ProductService(context, null, null);
            var result = service.GetProductListAsync("category1").Result;
            Assert.IsType<ResponseData<ProductListModel<Product>>>(result);
            Assert.True(result.Success);
            Assert.Equal(1, result.Data.CurrentPage);
            Assert.Equal(2, result.Data.Items.Count);
            Assert.Equal(1, result.Data.TotalPages);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(0), result.Data.Items[0]);
            Assert.Equal(context.Products.AsEnumerable().ElementAt(1), result.Data.Items[1]);
        }

        [Fact]
        public void ServiceSetsMaxPageSize()
        {
            using var context = CreateContext();
            var service = new ProductService(context, null, null);
            var result = service.GetProductListAsync(null, 1, 23).Result;
            Assert.IsType<ResponseData<ProductListModel<Product>>>(result);
            Assert.True(result.Success);
            Assert.Equal(1, result.Data.CurrentPage);
            Assert.Equal(20, result.Data.Items.Count);
            Assert.Equal(2, result.Data.TotalPages);
        }

        [Fact]
        public void ServiceReturnsErrorIfPageNoIsIncorrect()
        {
            using var context = CreateContext();
            var service = new ProductService(context, null, null);
            var result = service.GetProductListAsync(null, 4, 23).Result;
            Assert.IsType<ResponseData<ProductListModel<Product>>>(result);
            Assert.False(result.Success);
        }
    }
}

