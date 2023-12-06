using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using WEB_153503_KISELEVA.API.Data;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.API.Services
{
	public class ProductService : IProductService
	{
        private AppDbContext _context;
        private readonly int _maxPageSize = 20;
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _environment;

        public ProductService(AppDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        public async Task<ResponseData<Product>> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ResponseData<Product>
            {
                Data = product,
                Success = true
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                var res = new ResponseData<Product>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "Нет такого товара"
                };
                return res;
            }
            var result =  new ResponseData<Product>
            {
                Data = product,
                Success = true
            };
            return result;
        }

        public Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }
                
            var query = _context.Products.ToList();
            var dataList = new ProductListModel<Product>();

            query = query.Where(product => categoryNormalizedName == null || product.CategoryNormalizedName.Equals(categoryNormalizedName)).ToList();

            var count = query.Count();
            if (count == 0)
            {
                var result = new ResponseData<ProductListModel<Product>>
                {
                    Data = dataList
                };
                return Task.FromResult(result);
            }

            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageNo > totalPages)
            {
                var result = new ResponseData<ProductListModel<Product>>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "Нет такой страницы"
                };
                return Task.FromResult(result);
            }
                
            dataList.Items = query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;
            var response = new ResponseData<ProductListModel<Product>>
            {
                Data = dataList
            };
            return Task.FromResult(response);
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            var responseData = new ResponseData<string>();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                responseData.Success = false;
                responseData.ErrorMessage = "Нет такого товара";
                return responseData;
            }
            var host = "https://" + _httpContextAccessor.HttpContext.Request.Host;
            var imageFolder = Path.Combine(_environment.WebRootPath, "Images");
            if (formFile != null)
            {
                // Удалить предыдущее изображение
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var prevImage = Path.GetFileName(product.Image);
                    var prevImagePath = Path.Combine(imageFolder, prevImage);

                    if (File.Exists(prevImagePath))
                    {
                        File.Delete(prevImagePath);
                    }
                }
                // Создать имя файла
                var ext = Path.GetExtension(formFile.FileName);
                var fName = Path.ChangeExtension(Path.GetRandomFileName(), ext);
                // Сохранить файл
                using (var fileStream = new FileStream($"{imageFolder}/{fName}", FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                // Указать имя файла в объекте
                product.Image = $"{host}/Images/{fName}";
                await _context.SaveChangesAsync();
            }
            responseData.Data = product.Image;
            return responseData;
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            var prod = await _context.Products.FindAsync(id);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.Price = product.Price;
                if (product.Image is not null)
                {
                    prod.Image = product.Image;
                }
                prod.CategoryNormalizedName = product.CategoryNormalizedName;
               _context.Update(prod);
                await _context.SaveChangesAsync();
            }
        }
    }
}

