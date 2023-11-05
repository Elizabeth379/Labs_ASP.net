using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;
using WEB_153503_Kiseleva.Services.CategoryService;

namespace WEB_153503_Kiseleva.Services.ProductService
{
    public class MemoryProductService: IProductService
    {
        List<Book> _books;
        List<Category> _categories;
        int _pageSize;
        IConfiguration _config;

        public MemoryProductService(IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync()
            .Result
            .Data;
            _pageSize = config.GetValue<int>("ItemsPerPage");
            _config = config;
            SetupData();
        }


        private void SetupData()
        {
            _books = new List<Book>
        {
            new Book {Id = 1, Name="Wiedzmin",
                Description="Fantasy book with interesting huge world",
                Price =1000, Image="Images/Wiedzmin.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("fantasy"))},
            new Book { Id = 2, Name="Reflection maze",
                Description="Sience fiction book about virtual world",
                Price =800, Image="Images/ReflectionMaze.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("science_fiction"))},
            new Book { Id = 3, Name="Master and Margarita",
                Description="Classic novel about life and love",
                Price =700, Image="Images/MasterAndMargarita.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("novel"))},
            new Book { Id = 4, Name="Buttermilk",
                Description="Interesting fairy tale for children",
                Price =400, Image="Images/Buttermilk.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("fairy_tale"))},
            new Book { Id = 5, Name="Borodino",
                Description="Poem about war 1812",
                Price =200, Image="Images/Borodino.jpg",
                Category=_categories.Find(c=>c.NormalizedName.Equals("poetry"))},
        };
        }
        public Task<ResponseData<ListModel<Book>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var itemsPerPage = int.Parse(_config["ItemsPerPage"]);
            var items = _books
                .Where(d => categoryNormalizedName == null || d.Category.NormalizedName.Equals(categoryNormalizedName));

            var result = new ResponseData<ListModel<Book>>()
            {
                Data = new()
                {
                    Items = items.Skip(itemsPerPage * (pageNo - 1)).Take(3).ToList(),
                    CurrentPage = pageNo,
                    TotalPages = (items.Count() + itemsPerPage - 1) / itemsPerPage,
                }
            };
            return Task.FromResult(result);
        }
    }
}
