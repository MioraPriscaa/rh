using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnPostValiderAsync(int candidatureId)
        {
            

            var candidature = await _context.Candidature
                .Include(c => c.Annonce)
                .Include(c => c.Statut)
                .FirstOrDefaultAsync(c => c.Id == candidatureId);

            if (candidature == null || candidature.Annonce == null)
                return NotFound("Candidature ou Annonce introuvable");

            candidature.IdStatut = 2; // Validé
            candidature.Annonce.NbDossierValide += 1;

            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = candidature.Annonce.Id });
        }

        public async Task<IActionResult> OnPostRefuserAsync(int candidatureId)
        {
            var candidature = await _context.Candidature
                .Include(c => c.Annonce)
                .FirstOrDefaultAsync(c => c.Id == candidatureId);

            if (candidature == null || candidature.Annonce == null)
                return NotFound("Candidature ou Annonce introuvable");

            candidature.IdStatut = 5; // Refusé
            await _context.SaveChangesAsync();

            return RedirectToPage(new { id = candidature.Annonce.Id });
        }

        public async Task<IActionResult> OnPostExportExcelAsync(int id)
        {
            // Recharger l'annonce avec les relations nécessaires
            Annonce = await _context.Annonces
                .Include(a => a.Candidatures)
                    .ThenInclude(c => c.Candidat)
                .Include(a => a.Candidatures)
                    .ThenInclude(c => c.Statut)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Annonce == null)
            {
                return Content("Annonce non trouvée !");
            }

            if (Annonce.Candidatures == null || !Annonce.Candidatures.Any())
            {
                return Content("Aucune candidature liée à cette annonce !");
            }

            foreach (var c in Annonce.Candidatures)
            {
                var nomCandidat = c.Candidat?.Nom ?? "Candidat null";
                var statut = c.Statut?.Libelle ?? "Statut null";
                Console.WriteLine($"{nomCandidat} - {statut}");
            }


            var acceptes = Annonce.Candidatures
                .Where(c => c.Statut != null && c.Candidat != null && c.Statut.Id == 2)
                .Select(c => new
                {
                    Nom = c.Candidat.Nom ?? "",
                    Prenom = c.Candidat.Prenom ?? "",
                    Email = c.Candidat.Email ?? "",
                    Statut = c.Statut.Libelle
                })
                .ToList();

            if (!acceptes.Any())
            {
                return BadRequest("Aucune candidature acceptée à exporter.");
            }

            var sb = new StringBuilder();
            sb.AppendLine("Nom,Prenom,Email,Statut");
            foreach (var c in acceptes)
            {
                sb.AppendLine($"{c.Nom},{c.Prenom},{c.Email},{c.Statut}");
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(sb.ToString());
            string fileName = "CandidatsAcceptes.csv";

            return File(fileBytes, "text/csv", fileName);
        }




    }
}
