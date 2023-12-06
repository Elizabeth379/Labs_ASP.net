using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;
using WEB_153503_KISELEVA.Services.ProductService;

namespace WEB_153503_KISELEVA.Areas.Admin.Pages.Products
{
    public class IndexModel : PageModel
    {
        private IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public ProductListModel<Product> Product { get;set; } = new ProductListModel<Product>();

        public async Task OnGetAsync(int pageNo=1)
        {
            var productsResponse = await _productService.GetProductListAsync(null, pageNo);
            if (productsResponse.Success)
            {
                Product = productsResponse.Data!;
            }
        }
    }
}
