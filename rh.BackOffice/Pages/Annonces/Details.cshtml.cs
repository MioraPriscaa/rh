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
    public class DetailsModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public DetailsModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public Annonce Annonce { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Annonce = await _context.Annonces
                .Include(a => a.TypeContrat)
                .Include(a => a.ModeTravail)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Annonce == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
