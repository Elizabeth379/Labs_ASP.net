using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;

namespace WEB_153503_Kiseleva.API.Services
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();

    }
}
