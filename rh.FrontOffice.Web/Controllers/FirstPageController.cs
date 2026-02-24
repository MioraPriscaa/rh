using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.FrontOffice.Web.Controllers
{
    public class FirstPageController : Controller
    {
        private readonly AppDbContext _context;

        public FirstPageController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("~/Views/FirstPages/Index.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveAnnonces()
        {
            var today = DateTime.Today;
            int? idcandidat = HttpContext.Session.GetInt32("UserId");

            if (idcandidat == null)
            {
                var annonces = await _context.Annonces
                    .Where(a => a.DateFin == null || a.DateFin >= today)
                    .ToListAsync();
                return View("~/Views/FirstPages/Index.cshtml", annonces);
            }
            var annoncesCandidat = await _context.Annonces
                .Where(a => (a.DateFin == null || a.DateFin >= today) &&
                !_context.Candidature.Any(c => c.IdAnnonce == a.Id && c.IdCandidat == idcandidat))
                .ToListAsync();
            ViewBag.iduser = idcandidat;
            return View("~/Views/FirstPages/Index.cshtml", annoncesCandidat);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            int? idcandidat = HttpContext.Session.GetInt32("UserId");

            var annonce = await _context.Annonces
                .Include(a => a.ModeTravail)
                .Include(a => a.TypeContrat)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (annonce == null)
                return NotFound();
            ViewBag.iduser = idcandidat;

            return View("~/Views/FirstPages/Details.cshtml", annonce);
        }

    }
}
