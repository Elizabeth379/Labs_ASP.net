using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.API.Data;
using WEB_153503_KISELEVA.API.Services;
using WEB_153503_KISELEVA.Domain.Entities;
using WEB_153503_KISELEVA.Domain.Models;

namespace WEB_153503_KISELEVA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private string _imagesPath;
        private string _appUri;
        private ILogger _logger;

        public ProductsController(IProductService productService, IWebHostEnvironment env, IConfiguration configuration, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
            _imagesPath = Path.Combine(env.WebRootPath, "Images");
            _appUri = configuration.GetSection("appUri").Value;
        }

        // POST: api/Products/5
        [HttpPost("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseData<string>>> PostImage(int id, IFormFile formFile)

        {
            var response = await _productService.SaveImageAsync(id, formFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        // GET: api/Products
        [HttpGet]
        [HttpGet("{category}")]
        [HttpGet("page{pageNo:int}")]
        [HttpGet("size{pageSize:int}")]
        [HttpGet("{category}/page{pageNo:int}")]
        [HttpGet("{category}/size{pageSize:int}")]
        [HttpGet("page{pageNo:int}/size{pageSize:int}")]
        [HttpGet("{category}/page{pageNo:int}/size{pageSize:int}")]
        [Authorize]
        public async Task<ActionResult<ResponseData<List<Product>>>> GetProducts(string? category, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _productService.GetProductListAsync(category, pageNo, pageSize));
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return Ok(await _productService.GetProductByIdAsync(id));
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            await _productService.UpdateProductAsync(id, product);
            return Ok();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            return Ok(await _productService.CreateProductAsync(product));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
