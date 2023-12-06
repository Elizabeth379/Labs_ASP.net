using System;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.Services.CategoryService
{
	public interface ICategoryService
	{
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}

