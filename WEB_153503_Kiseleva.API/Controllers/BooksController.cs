using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Kiseleva.API.Data;
using WEB_153503_Kiseleva.API.Services;
using WEB_153503_Kiseleva.Domain.Entities;
using WEB_153503_Kiseleva.Domain.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WEB_153503_Kiseleva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IProductService _productService;

        public BooksController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/Books
        [HttpGet("{pageNo:int}")]
        [HttpGet("{category?}/{pageNo:int?}")]

        public async Task<ActionResult<ResponseData<List<Book>>>> GetBooks(string? category, int pageNo = 1, int pageSize = 3)
        {
            return Ok(await _productService.GetProductListAsync(category, pageNo, pageSize));
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Book>>> GetBook(int id)
        {
            var res = await _productService.GetProductByIdAsync(id);
            return res.Success ? Ok(res) : NotFound(res);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseData<Book>>> PutBook(int id, Book book)
        {
            try
            {
                await _productService.UpdateProductAsync(id, book);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseData<Book>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new ResponseData<Book>()
            {
                Data = book
            });
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ResponseData<Book>>> PostBook(Book book)
        {
            var res = await _productService.CreateProductAsync(book);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        // DELETE: api/Books/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseData<Book>()
                {
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return NoContent();
        }

        // POST: api/Books/5
        [Authorize(Roles = "Admin")]
        [HttpPost("{id}")]
        public async Task<ActionResult<ResponseData<string>>> PostImage(int id, IFormFile formFile)
        {
            var response = await _productService.SaveImageAsync(id, formFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        private async Task<bool> BookExists(int id)
        {
            return (await _productService.GetProductByIdAsync(id)).Success;
        }
    }
}
