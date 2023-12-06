using System;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.Services.ProductService
{
	public interface IProductService
	{
        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(string? categoryNormalizedName = null, int pageNo = 1);
        public Task<ResponseData<Product>> GetProductByIdAsync(int id);
        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile);
        public Task DeleteProductAsync(int id);
        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile);
    }
}

