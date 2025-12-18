using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Pages_Annonces
{
    public class EditModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public EditModel(rh.Infrastructure.Data.AppDbContext context)
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

            var annonce =  await _context.Annonces.FirstOrDefaultAsync(m => m.Id == id);
            if (annonce == null)
            {
                return NotFound();
            }
            Annonce = annonce;
           ViewData["IdModeTravail"] = new SelectList(_context.ModeTravails, "Id", "Libelle");
           ViewData["IdTypeContrat"] = new SelectList(_context.TypeContrats, "Id", "Libelle");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Annonce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnonceExists(Annonce.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AnnonceExists(int id)
        {
            return _context.Annonces.Any(e => e.Id == id);
        }
    }
}
