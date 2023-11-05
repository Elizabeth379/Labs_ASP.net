using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153503_Kiseleva.API.Data;
using WEB_153503_Kiseleva.Domain.Entities;

namespace WEB_153503_Kiseleva.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WEB_153503_Kiseleva.API.Data.AppDbContext _context;

        public DetailsModel(WEB_153503_Kiseleva.API.Data.AppDbContext context)
        {
            _context = context;
        }

      public Book Book { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else 
            {
                Book = book;
            }
            return Page();
        }
    }
}
