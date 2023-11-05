using Microsoft.EntityFrameworkCore;
using WEB_153503_Kiseleva.Domain.Entities;

namespace WEB_153503_Kiseleva.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            //Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //Выполнение миграций
            await context.Database.MigrateAsync();

            await context.Categories.AddRangeAsync(new List<Category>()
            {
                new Category {Name="Fantasy", NormalizedName="fantasy"},
                new Category {Name="Science fiction", NormalizedName="science_fiction"},
                new Category {Name="Novel", NormalizedName="novel"},
                new Category {Name="Fairy tale", NormalizedName="fairy_tale"},
                new Category {Name="Poetry", NormalizedName="poetry"}
            });
            await context.SaveChangesAsync();

            string imageRoot = $"{app.Configuration["AppUrl"]}/Images";
            await context.Books.AddRangeAsync(new List<Book>()
            {
            new Book {Name="Wiedzmin",
                Description="Fantasy book with interesting huge world",
                Category = await context.Categories.SingleAsync(c=>c.NormalizedName.Equals("fantasy")),
                Price =1000, Image=$"{imageRoot}/Wiedzmin.jpg"},
            new Book { Name="Reflection maze",
                Description="Sience fiction book about virtual world",
                Category = await context.Categories.SingleAsync(c => c.NormalizedName.Equals("science_fiction")),
                Price =800, Image=$"{imageRoot}/ReflectionMaze.jpg"},
            new Book { Name="Master and Margarita",
                Description="Classic novel about life and love",
                Category=await context.Categories.SingleAsync(c=>c.NormalizedName.Equals("novel")),
                Price =700, Image=$"{imageRoot}/MasterAndMargarita.jpg"},
            new Book { Name="Buttermilk",
                Description="Interesting fairy tale for children",
                 Category=await context.Categories.SingleAsync(c=>c.NormalizedName.Equals("fairy_tale")),
                Price =400, Image=$"{imageRoot}/Buttermilk.jpg"},
            new Book { Name="Borodino",
                Description="Poem about war 1812",
                Category=await context.Categories.SingleAsync(c=>c.NormalizedName.Equals("poetry")),
                Price =200, Image=$"{imageRoot}/Borodino.jpg"},
            });
            await context.SaveChangesAsync();
        }
    }
}
