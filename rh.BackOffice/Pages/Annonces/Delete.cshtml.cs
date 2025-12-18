using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Pages_Annonces
{
    public class DeleteModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public DeleteModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Annonce Annonce { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces.FirstOrDefaultAsync(m => m.Id == id);

            if (annonce == null)
            {
                return NotFound();
            }
            else
            {
                Annonce = annonce;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce != null)
            {
                Annonce = annonce;
                _context.Annonces.Remove(Annonce);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
