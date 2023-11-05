using WEB_153503_Kiseleva.Domain.Models;
using WEB_153503_Kiseleva.Domain.Entities;

namespace WEB_153503_Kiseleva.API.Services
{
    public interface IProductService
    {
        public Task<ResponseData<ListModel<Book>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3);
        public Task<ResponseData<Book>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Book book);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Book>> CreateProductAsync(Book book);
        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }
}
