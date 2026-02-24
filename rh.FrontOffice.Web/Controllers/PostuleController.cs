using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;
using System.IO;
using System.Net.NetworkInformation;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace rh.FrontOffice.Web.Controllers
{
    public class PostuleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PostuleController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //getquestionnaire 
        //question : est ce que vous voulez importer un nouveau CV ou garder l'ancien pour postuler à l'annonce idannonce?
        // à récupérer : idcandidat depuis la session, idannonce depuis le paramètre

        //à afficher dans vue : cv candidat (s'il existe), idannonce, bref blabla

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

            // Remplacement de la ligne problématique pour gérer le cas où PieceJointe peut être null
            string nomFichier = candidat.PieceJointe != null ? Path.GetFileName(candidat.PieceJointe) : string.Empty;

            ViewBag.PieceJointe = nomFichier;
            return View("~/Views/Postule/Index.cshtml");
        }


        //s'il a uploade un nouveau CV dia ito no antsoiny, sinon miova mintsy ny CV-any
        //on va modifier le nom du fichier avec son nom et son id , dia raha efa misy ao dia remplacena
        [HttpPost]
        public async Task<IActionResult> UploadCV(IFormFile newcv)
        {
            if (newcv != null && newcv.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "cvs", "Téléchargements");
                Directory.CreateDirectory(uploads); // S'assure que le dossier existe

                var filePath = Path.Combine(uploads, Path.GetFileName(newcv.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await newcv.CopyToAsync(stream);
                }
                // Traitez la suite (redirection, message, etc.)
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("create")]
        public async Task<IActionResult> CreateCandidature(int idAnnonce, IFormFile? newcv)
        {
            int? idcandidat = HttpContext.Session.GetInt32("UserId");
            if (idcandidat == null)
            {
                return RedirectToAction("LoginBasic", "Auth");
            }

            if (newcv != null)
            {
                UpdateCV(newcv);
            }

            var candidature = new Candidature
            {
                IdAnnonce = idAnnonce,
                IdCandidat = idcandidat.Value,
                IdStatut = 1
            };

            _context.Candidature.Add(candidature);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetActiveAnnonces", "FirstPage");
        }

        private void UpdateCV(IFormFile newcv)
        {
            int? idcandidat = HttpContext.Session.GetInt32("UserId");
            if (idcandidat == null)
                return;

            var candidat = _context.Candidat.FirstOrDefault(c => c.Id == idcandidat);
            if (candidat == null)
                return;

            string nom = candidat.Nom ?? "";
            string prenom = candidat.Prenom ?? "";
            string safeNom = string.Concat(nom.Where(char.IsLetterOrDigit));
            string safePrenom = string.Concat(prenom.Where(char.IsLetterOrDigit));
            string extension = Path.GetExtension(newcv.FileName);
            string newFileName = $"{safeNom}_{safePrenom}{extension}";

            var parentDirectory = Directory.GetParent(_env.ContentRootPath);
            if (parentDirectory == null)
                return;

            var uploads = Path.Combine(parentDirectory.FullName, "rh.BackOffice", "wwwroot", "cvs", "Téléchargements");
            Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                newcv.CopyTo(stream);
            }

            candidat.PieceJointe = $"wwwroot/cvs/Téléchargements/{newFileName}";
            _context.Candidat.Update(candidat);
            _context.SaveChanges();
        }

        public IActionResult ApercuCV(string pj)
        {
            var pieceJointe = pj;
            if (string.IsNullOrEmpty(pieceJointe))
                return NotFound();
            var parentDirectory = Directory.GetParent(_env.ContentRootPath);
            if (parentDirectory == null)
                return NotFound();

            var filePath = Path.Combine(parentDirectory.FullName, "rh.BackOffice", "wwwroot", "cvs", "Téléchargements", Path.GetFileName(pieceJointe));
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var contentType = "application/octet-stream";
            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext == ".pdf") contentType = "application/pdf";
            else if (ext == ".doc" || ext == ".docx") contentType = "application/msword";
            else if (ext == ".jpg" || ext == ".jpeg") contentType = "image/jpeg";
            else if (ext == ".png") contentType = "image/png";

            return PhysicalFile(filePath, contentType);
        }
    }
}
