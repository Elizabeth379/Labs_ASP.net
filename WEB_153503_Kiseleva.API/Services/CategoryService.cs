using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WEB_153503_Kiseleva.API.Data;
using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;

namespace WEB_153503_Kiseleva.API.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            return new ResponseData<List<Category>>
            {
                Data = await _context.Categories.ToListAsync()
            };
        }
    }
}
