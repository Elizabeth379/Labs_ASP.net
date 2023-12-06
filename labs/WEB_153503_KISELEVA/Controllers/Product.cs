using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;
using WEB_153503_KISELEVA.Extensions;
using WEB_153503_KISELEVA.Services.CategoryService;
using WEB_153503_KISELEVA.Services.ProductService;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WEB_153503_KISELEVA.Controllers
{
    [Route("catalog")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("{category?}")]
        public async Task<IActionResult> Index(string? category, int pageNo = 1)
        {
            var categoryResponse = await _categoryService.GetCategoryListAsync();
            if (!categoryResponse.Success)
            {
                return NotFound(categoryResponse.ErrorMessage);
            }

            ViewData["categories"] = categoryResponse.Data;
            ViewData["currentCategory"] = category == null ? "Все" : categoryResponse.Data!.FirstOrDefault((c) => c.NormalizedName.Equals(category))?.Name;

            var productResponse = await _productService.GetProductListAsync(category, pageNo);
            

            if (!productResponse.Success)
            {
                return NotFound(productResponse.ErrorMessage);
            }

            var data = productResponse.Data;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListPartial", data);
            }
            return View(data);
        }
    }
}

