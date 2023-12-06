using System;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.Domain.Entities;

namespace WEB_153503_KISELEVA.API.Data
{
	public class DbInitializer
	{
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            if (!context.Products.Any())
            {
                var configuration = app.Configuration;
                var imageUrlBase = configuration["ImageUrlBase"];

                var products = new List<Product>
            {
                new Product{Name = "Ведьмак", Description = "Фэнтезийная книга с интересным волшебным миром, населенном разумными расами и монстрами", Price = 7.10, Image = imageUrlBase + "ТеннисныйМяч.jpeg", CategoryNormalizedName = "fantasy"},
                new Product{Name = "Путь меча", Description = "Меч и его человек.", Price = 10.0, Image = imageUrlBase + "ПлюшевыйМишка.jpeg", CategoryNormalizedName = "fantasy" },
                new Product{Name = "Лабиринт отражений", Description = "Книга о виртуальной реальности, в которую постепенно переходит человечество", Price = 9.0, Image = imageUrlBase + "ТаблеткиКруглые.jpeg", CategoryNormalizedName = "science_fiction"},
                new Product{Name = "Дюна", Description = "В центре повествования пустынная планета Арракис", Price = 15.60, Image = imageUrlBase + "ТаблеткиОвальные.jpeg", CategoryNormalizedName = "science_fiction"},
                new Product{Name = "Мастер и Маргарита", Description = "Роман о жизни и любви", Price = 33.70, Image = imageUrlBase + "ПлащЖёлтый.jpeg", CategoryNormalizedName = "novel"},
                new Product{Name = "Благословение небожителей", Description = "Фэнтезийный роман основанный на мифологии Китая", Price = 28.70, Image = imageUrlBase + "СвитерВязаныйРозовый.jpeg", CategoryNormalizedName = "novel"},
                new Product{Name = "Простоквашино", Description = "Детская сказка о самостоятельном мальчике и его питомцах", Price = 6.60, Image = imageUrlBase + "Косточка.jpeg", CategoryNormalizedName = "fairy_tale"},
                new Product{Name = "Сказки братьев Гримм", Description = "Сборник известных детских сказок братьев Гримм", Price = 185.20, Image = imageUrlBase + "КормРазноцветный.jpeg", CategoryNormalizedName = "fairy_tale"},
                new Product{Name = "Бородино", Description = "Поэма о войне 1812 года", Price = 12.20, Image = imageUrlBase + "ОшейникГолубойСПодвеской.jpeg", CategoryNormalizedName = "poetry"},
                new Product{Name = "Руслан и Людмила", Description = "Поэма Пушкина", Price = 12.20, Image = imageUrlBase + "ОшейникЗелёныйСПодвеской.jpeg", CategoryNormalizedName = "poetry"},
            };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                new Category{Name = "Фэнтези", NormalizedName = "fantasy"},
                new Category{Name = "Научная фантастика", NormalizedName = "science_fiction"},
                new Category{Name = "Роман", NormalizedName = "novel"},
                new Category{Name = "Сказка", NormalizedName = "fairy_tale"},
                new Category{Name = "Поэзия", NormalizedName = "poetry"},
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
        }

    }
}

