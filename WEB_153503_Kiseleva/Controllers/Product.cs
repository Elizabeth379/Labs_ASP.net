using Microsoft.AspNetCore.Mvc;
using WEB_153503_Kiseleva.Services.CategoryService;
using WEB_153503_Kiseleva.Services.ProductService;


namespace WEB_153503_Kiseleva.Controllers
{
    public class Product : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public Product(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> GetProduct(string categoryNormalizedName, int pageNo = 1)
        {
            var productListResponse = await _productService.GetProductListAsync(categoryNormalizedName, pageNo);

            return View(productListResponse.Data);
        }

        public async Task<IActionResult> Index(string? category, int pageno)
        {
            var productResponse = await _productService.GetProductListAsync(category, pageno);
            if (!productResponse.Success)
                return NotFound(productResponse.ErrorMessage);

            var categoriesResponse = await _categoryService.GetCategoryListAsync();
            if (!categoriesResponse.Success)
                return NotFound(productResponse.ErrorMessage);

            ViewData["currentCategory"] = category == null ? "Всё" : categoriesResponse.Data.Find(arg => arg.NormalizedName == category).Name;
            ViewData["categories"] = categoriesResponse.Data;
            return View(productResponse.Data);
        }
    }
}
