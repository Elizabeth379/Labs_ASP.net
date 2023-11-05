using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;

namespace WEB_153503_Kiseleva.Services.CategoryService
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();

    }
}
