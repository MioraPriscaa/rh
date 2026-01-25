using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rh.Infrastructure.Data;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace rh.FrontOffice.Web.Controllers
{
    public class PostuleController : Controller
    {
        private readonly AppDbContext _context;

        public PostuleController(AppDbContext context)
        {
            _context = context;
        }

        //getquestionnaire 
        //question : est ce que vous voulez importer un nouveau CV ou garder l'ancien pour postuler à l'annonce idannonce?
        // à récupérer : idcandidat depuis la session, idannonce depuis le paramètre

        //à afficher dans vue : cv candidat (s'il existe), idannonce

        [HttpGet]
        public async Task<IActionResult> Postule(int idannonce)
        {
            int? idcandidat = HttpContext.Session.GetInt32("UserId");
            if (idcandidat == null)
            {
                return RedirectToAction("LoginBasic", "Auth");
            }
            var candidat = await _context.Candidat
                .FirstOrDefaultAsync(c => c.Id == idcandidat);
            if (candidat == null)
            {
                return NotFound("Candidat non trouvé.");
            }
            var annonce = await _context.Annonces
                .Include(a => a.ModeTravail)
                .Include(a => a.TypeContrat)
                .FirstOrDefaultAsync(m => m.Id == idannonce);
            ViewBag.TitleAnnonce = annonce?.Libelle;
            ViewBag.IdAnnonce = idannonce;
            ViewBag.PieceJointe = candidat.PieceJointe;
            return View("~/Views/Postule/Index.cshtml");
        }

    }
}
