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
                new Product{Name = "Мячик Теннисный", Description = "Способствует развитию физической активности и челюстной мускулатуры", Price = 7.10, Image = imageUrlBase + "ТеннисныйМяч.jpeg", CategoryNormalizedName = "toys"},
                new Product{Name = "Плюшевый Мишка", Description = "Мягкая игрушка для собак в виде забавного медвежонка.", Price = 10.0, Image = imageUrlBase + "ПлюшевыйМишка.jpeg", CategoryNormalizedName = "toys" },
                new Product{Name = "Кормовая добавка", Description = "Предназначена для нормализации обмена веществ и поддержания иммунитета у собак", Price = 9.0, Image = imageUrlBase + "ТаблеткиКруглые.jpeg", CategoryNormalizedName = "medicines"},
                new Product{Name = "Пребиотик", Description = "Восстанавливает полезную микрофлору кишечника и укрепляет иммунитет", Price = 15.60, Image = imageUrlBase + "ТаблеткиОвальные.jpeg", CategoryNormalizedName = "medicines"},
                new Product{Name = "Плащ жёлтый", Description = "Удобный и функциональный аксессуар для собак, который просто незаменим в дождливую погоду", Price = 33.70, Image = imageUrlBase + "ПлащЖёлтый.jpeg", CategoryNormalizedName = "clothes"},
                new Product{Name = "Свитер розовый", Description = "Тёплый, удобный и функциональный аксессуар для собак, который просто незаменим в холодную погоду", Price = 28.70, Image = imageUrlBase + "СвитерВязаныйРозовый.jpeg", CategoryNormalizedName = "clothes"},
                new Product{Name = "Косточка из оленины", Description = "Косточка для чистки зубов из оленины", Price = 6.60, Image = imageUrlBase + "Косточка.jpeg", CategoryNormalizedName = "food"},
                new Product{Name = "Сухой корм для взрослых собак всех пород", Description = "Cодержит все белки, необходимые для развития мышц и тканей", Price = 185.20, Image = imageUrlBase + "КормРазноцветный.jpeg", CategoryNormalizedName = "food"},
                new Product{Name = "Ошейник голубой с подвеской", Description = "Надежный помощник в выгуле собак, регулируемый", Price = 12.20, Image = imageUrlBase + "ОшейникГолубойСПодвеской.jpeg", CategoryNormalizedName = "collars"},
                new Product{Name = "Ошейник зелёный с подвеской", Description = "Надежный помощник в выгуле собак, регулируемый", Price = 12.20, Image = imageUrlBase + "ОшейникЗелёныйСПодвеской.jpeg", CategoryNormalizedName = "collars"},
            };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                new Category{Name = "Игрушки", NormalizedName = "toys"},
                new Category{Name = "Лекарства", NormalizedName = "medicines"},
                new Category{Name = "Одежда", NormalizedName = "clothes"},
                new Category{Name = "Еда", NormalizedName = "food"},
                new Category{Name = "Ошейники", NormalizedName = "collars"},
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }
        }

    }
}

