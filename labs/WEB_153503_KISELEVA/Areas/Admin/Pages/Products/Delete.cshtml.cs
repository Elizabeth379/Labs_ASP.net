using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Services.ProductService;

namespace WEB_153503_KISELEVA.Areas.Admin.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _productService.GetProductByIdAsync((int)id);

            if (!response.Success)
            {
                return NotFound();
            }

            Product = response.Data;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _productService.DeleteProductAsync((int)id);

            return RedirectToPage("./Index");
        }
    }
}
