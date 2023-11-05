using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;

namespace WEB_153503_Kiseleva.Services.CategoryService
{
    public class MemoryCategoryService: ICategoryService
    {
        public Task<ResponseData<List<Category>>>
       GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Fantasy", NormalizedName="fantasy"},
                new Category {Id=2, Name="Science fiction", NormalizedName="science_fiction"},
                new Category {Id=3, Name="Novel", NormalizedName="novel"},
                new Category {Id=4, Name="Fairy tale", NormalizedName="fairy_tale"},
                new Category {Id=5, Name="Poetry", NormalizedName="poetry"},
            };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}
