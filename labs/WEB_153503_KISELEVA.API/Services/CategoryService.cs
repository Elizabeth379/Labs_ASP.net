using System;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.API.Data;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.API.Services
{
	public class CategoryService : ICategoryService
	{
        private AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return new ResponseData<List<Category>> { Data = categories };
        }
    }
}

