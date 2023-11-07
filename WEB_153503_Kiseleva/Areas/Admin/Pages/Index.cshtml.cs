using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Kiseleva.API.Data;
using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Services.ProductService;
using WEB_153503_Kiseleva.Services.CategoryService;

namespace WEB_153503_Kiseleva.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Book> Books { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNo = 1)
        {
            var responce = await _productService.GetProductListAsync(null, pageNo);

            if (!responce.Success)
                return NotFound(responce.ErrorMessage ?? "");


            Books = responce.Data?.Items!;
            CurrentPage = responce.Data?.CurrentPage ?? 0;
            CurrentPage = responce.Data?.TotalPages ?? 0;

            return Page();

        }
    }
}
