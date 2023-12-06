using System;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.Services.CategoryService
{
	public class MemoryCategoryService : ICategoryService
	{
        private List<Category> _categories = new List<Category>
        {
                new Category{Id = 1, Name = "Фэнтези", NormalizedName = "fantasy"},
                new Category{Id = 2, Name = "Научная фантастика", NormalizedName = "science_fiction"},
                new Category{Id = 3, Name = "Одежда", NormalizedName = "novel"},
                new Category{Id = 4, Name = "Сказка", NormalizedName = "fairy_tale"},
                new Category{Id = 5, Name = "Поэзия", NormalizedName = "poetry"},
        };

        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var result = new ResponseData<List<Category>> { Data = _categories };
            return Task.FromResult(result);
        }

        public Task<ResponseData<Category>> GetCategoryByNormalizedNameAsync(string normalizedName)
        {
            var category = _categories.First((cat) => cat.NormalizedName.Equals(normalizedName));
            var result = new ResponseData<Category> { Data = category };
            return Task.FromResult(result);
        }
    }
}

