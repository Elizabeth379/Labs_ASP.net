using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Services.CategoryService;
using WEB_153503_KISELEVA.Services.ProductService;

namespace WEB_153503_KISELEVA.Areas.Admin.Pages.Products
{
    public class CreateModel : PageModel
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public CreateModel(IProductService prodctService, ICategoryService categoryService)
        {
            _productService = prodctService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _categoryService.GetCategoryListAsync();
            ViewData["categories"] = categories;
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid || Product == null)
            {
                return Page();
            }

            Debug.WriteLine($"AAAAAAAA {Product.Name}, {Product.Description}, {Product.Price}");
            Debug.WriteLine($"IMAGE {Image}");
            await _productService.CreateProductAsync(Product, Image);

            return RedirectToPage("./Index");
        }
    }
}
