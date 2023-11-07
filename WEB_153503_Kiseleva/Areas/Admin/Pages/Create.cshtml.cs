using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153503_Kiseleva.API.Data;
using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Services.ProductService;
using WEB_153503_Kiseleva.Services.CategoryService;

namespace WEB_153503_Kiseleva.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _productService.CreateProductAsync(Book, Image);
            if (!response.Success)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
