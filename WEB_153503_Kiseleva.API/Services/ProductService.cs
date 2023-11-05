using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using WEB_153503_Kiseleva.API.Data;
using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;

namespace WEB_153503_Kiseleva.API.Services
{
    public class ProductService: IProductService
    {
        private readonly int _maxPageSize = 20;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResponseData<Book>> CreateProductAsync(Book book)
        {
            _context.Books.Add(book);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseData<Book>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
            return new ResponseData<Book>
            {
                Data = book,
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book was not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<ResponseData<Book>> GetProductByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new()
                {
                    ErrorMessage = "Book was not found",
                    Success = false
                };
            }
            return new()
            {
                Data = book,
            };
        }

        public async Task<ResponseData<ListModel<Book>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
            {
                pageSize = _maxPageSize;
            }
            var query = _context.Books.AsQueryable();
            var dataList = new ListModel<Book>();

            query = query.Where(b => categoryNormalizedName == null
                                || b.Category.NormalizedName.Equals(categoryNormalizedName));
            // количество элементов в списке
            var count = await query.CountAsync();
            if (count == 0)
            {
                return new ResponseData<ListModel<Book>> { Data = dataList };
            }
            // количество страниц
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            if (pageNo > totalPages)
            {
                // количество страниц
                return new ResponseData<ListModel<Book>>
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = "No such page"
                };
            }
            dataList.Items = await query.Skip((pageNo - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
            dataList.TotalPages = totalPages;
            dataList.CurrentPage = pageNo;

            var response = new ResponseData<ListModel<Book>>
            {
                Data = dataList
            };
            return response;
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return new ResponseData<string>
                {
                    Success = false,
                    ErrorMessage = "Book was not found"
                };
            }

            string imageRoot = Path.Combine(_configuration["AppUrl"], "Images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;

            string imagePath = Path.Combine(imageRoot, uniqueFileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            book.Image = imagePath;
            await _context.SaveChangesAsync();

            return new ResponseData<string>
            {
                Data = book.Image,
                Success = true
            };
        }

        public async Task UpdateProductAsync(int id, Book book)
        {
            var oldBook = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new Exception("Book was not found");
            }
            oldBook.Name = book.Name;
            oldBook.Description = book.Description;
            oldBook.Category = book.Category;
            oldBook.Price = book.Price;
            oldBook.Image = book.Image;

            await _context.SaveChangesAsync();
        }
    }
}
