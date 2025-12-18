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
        // Liste des candidats qui ont postulé
        public List<Candidat> Candidats { get; set; } = new List<Candidat>();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Annonce = await _context.Annonces
                .Include(a => a.TypeContrat)
            .Include(a => a.ModeTravail)
     .Include(a => a.Candidatures)
         .ThenInclude(c => c.Candidat)
     .Include(a => a.Candidatures)
         .ThenInclude(c => c.Statut) 
     .FirstOrDefaultAsync(m => m.Id == id);

            if (Annonce == null)
                return NotFound();

            Candidats = Annonce.Candidatures?.Select(c => c.Candidat).ToList() ?? new List<Candidat>();


            return Page();
        }
    }
}
