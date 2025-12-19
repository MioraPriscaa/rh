using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Pages_Candidats
{
    public class DetailsModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public DetailsModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public Candidat Candidat { get; set; } = default!;

        // URL de retour dynamique
        public string? ReturnUrl { get; set; }
        public List<Candidature> Candidatures { get; set; } = new List<Candidature>();
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            var candidat = await _context.Candidat.FirstOrDefaultAsync(m => m.Id == id);
            if (candidat == null)
                return NotFound();

            Candidat = candidat;

            // Remplit ReturnUrl avec l'URL de la page précédente
            ReturnUrl = Request.Headers["Referer"].ToString();
            Candidatures = await _context.Candidature
            .Where(c => c.IdCandidat == candidat.Id)
            .Include(c => c.Annonce)
            .Include(c => c.Statut)
            .ToListAsync();

            return Page();
        }
    }
}
