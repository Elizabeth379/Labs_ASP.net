using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;

namespace WEB_153503_Kiseleva.Services.ProductService
{
    public interface IProductService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">нормализованное имя категории дляфильтрации</param>
        /// <param name="pageNo">номер страницы списка</param>
        /// <returns></returns>
        public Task<ResponseData<ListModel<Book>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Найденный объект или null, если объект не найден</returns>
    }
}
