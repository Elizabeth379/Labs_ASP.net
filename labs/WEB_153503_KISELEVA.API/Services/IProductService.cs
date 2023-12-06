using System;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.API.Services
{
	public interface IProductService
	{
        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3);
        public Task<ResponseData<Product>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Product product);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Product>> CreateProductAsync(Product product);
        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }
}

