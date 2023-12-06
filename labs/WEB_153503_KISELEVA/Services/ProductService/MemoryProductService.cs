using System;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;
using WEB_153503_KISELEVA.Services.CategoryService;

namespace WEB_153503_KISELEVA.Services.ProductService
{
    public class MemoryProductService : IProductService
	{
        private List<Product> _products = new();
        private int _itemsPerPage;

        public MemoryProductService([FromServices] IConfiguration config)
		{
            _itemsPerPage = config.GetValue<int>("ItemsPerPage");
            SetUpData();
		}

        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(string? categoryNormalizedName = null, int pageNo = 1)
        {
            var products = _products.Where((product) => categoryNormalizedName == null || product.CategoryNormalizedName.Equals(categoryNormalizedName)).ToList();
         

            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / _itemsPerPage);

            var selectedProducts = products
                .Skip((pageNo - 1) * _itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();

            var result = new ResponseData<ProductListModel<Product>> { Data = new ProductListModel<Product> { Items = selectedProducts, CurrentPage = pageNo, TotalPages = totalPages } };
            return Task.FromResult(result);
        }

        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        private void SetUpData()
        {
            _products = new List<Product>
            {
                new Product{Id = 1, Name = "Ведьмак", Description = "Фэнтезийная книга с интересным волшебным миром, населенном разумными расами и монстрами", Price = 7.10, Image = "Images/ТеннисныйМяч.jpeg", CategoryNormalizedName = "fantasy"},
                new Product{Id = 2, Name = "Путь меча", Description = "Меч и его человек.", Price = 10.0, Image = "Images/ПлюшевыйМишка.jpeg", CategoryNormalizedName = "fantasy" },
                new Product{Id = 3, Name = "Лабиринт отражений", Description = "Книга о виртуальной реальности, в которую постепенно переходит человечество", Price = 9.0, Image = "Images/ТаблеткиКруглые.jpeg", CategoryNormalizedName = "science_fiction"},
                new Product{Id = 4, Name = "Дюна", Description = "В центре повествования пустынная планета Арракис", Price = 15.60, Image = "Images/ТаблеткиОвальные.jpeg", CategoryNormalizedName = "science_fiction"},
                new Product{Id = 5, Name = "Мастер и Маргарита", Description = "Роман о жизни и любви", Price = 33.70, Image = "Images/ПлащЖёлтый.jpeg", CategoryNormalizedName = "novel"},
                new Product{Id = 6, Name = "Благословение небожителей", Description = "Фэнтезийный роман основанный на мифологии Китая", Price = 28.70, Image = "Images/СвитерВязаныйРозовый.jpeg", CategoryNormalizedName = "novel"},
                new Product{Id = 7, Name = "Простоквашино", Description = "Детская сказка о самостоятельном мальчике и его питомцах", Price = 6.60, Image = "Images/Косточка.jpeg", CategoryNormalizedName = "fairy_tale"},
                new Product{Id = 8, Name = "Сказки братьев Гримм", Description = "Сборник известных детских сказок братьев Гримм", Price = 185.20, Image = "Images/КормРазноцветный.jpeg", CategoryNormalizedName = "fairy_tale"},
                new Product{Id = 9, Name = "Бородино", Description = "Поэма о войне 1812 года", Price = 12.20, Image = "Images/ОшейникГолубойСПодвеской.jpeg", CategoryNormalizedName = "poetry"},
                new Product{Id = 10, Name = "Руслан и Людмила", Description = "Поэма Пушкина", Price = 12.20, Image = "Images/ОшейникЗелёныйСПодвеской.jpeg", CategoryNormalizedName = "poetry"},
            };
        }
    }
}

