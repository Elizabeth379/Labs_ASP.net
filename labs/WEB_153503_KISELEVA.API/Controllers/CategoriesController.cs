using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153503_KISELEVA.API.Data;
using WEB_153503_KISELEVA.API.Services;
using WEB_153503_KISELEVA.Domain.Entities;

namespace WEB_153503_KISELEVA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _categoryService.GetCategoryListAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            //if (_context.Categories == null)
            //{
            //    return NotFound();
            //}
            //  var category = await _context.Categories.FindAsync(id);

            //  if (category == null)
            //  {
            //      return NotFound();
            //  }

            //  return category;
            throw new NotImplementedException();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            //if (id != category.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(category).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!CategoryExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            throw new NotImplementedException();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            //if (_context.Categories == null)
            //{
            //    return Problem("Entity set 'AppDbContext.Categories'  is null.");
            //}
            //  _context.Categories.Add(category);
            //  await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            throw new NotImplementedException();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            //if (_context.Categories == null)
            //{
            //    return NotFound();
            //}
            //var category = await _context.Categories.FindAsync(id);
            //if (category == null)
            //{
            //    return NotFound();
            //}

            //_context.Categories.Remove(category);
            //await _context.SaveChangesAsync();

            //return NoContent();
            throw new NotImplementedException();
        }

        private bool CategoryExists(int id)
        {
            // return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
            throw new NotImplementedException();
        }
    }
}
