using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rh.BackOffice.ViewModels;
using rh.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace rh.BackOffice.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<AnnonceStatsVM> TopAnnonces { get; set; } = new();

        // Liste des filtres disponibles pour le dropdown
        public List<(string Value, string Label)> Filters { get; set; } = new()
    {
        ("TopActives", "Top 5 annonce actives"),
        ("TopTerminees", "Top 5 annonces terminées")
    };

        // Filtre actuellement sélectionné
        public string CurrentFilter { get; set; } = "TopActives";

        public async Task OnGetAsync(string? filter)
        {
            CurrentFilter = filter ?? "TopActives";

            var query = _context.Annonces
                .Include(a => a.Candidatures)
                .AsQueryable();

            if (CurrentFilter == "TopTerminees")
                query = query.Where(a => a.DateFin != null && a.DateFin <= DateTime.Now);
            else
                query = query.Where(a => a.DateFin == null || a.DateFin > DateTime.Now);

            TopAnnonces = query
                .Select(a => new AnnonceStatsVM
                {
                    AnnonceId = a.Id,
                    Libelle = a.Libelle,
                    NombreCandidatures = a.Candidatures.Count()
                })
                .OrderByDescending(a => a.NombreCandidatures)
                .Take(5)
                .ToList();
        }
    }



}
