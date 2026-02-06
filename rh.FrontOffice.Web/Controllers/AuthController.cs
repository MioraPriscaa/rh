using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.FrontOffice.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult LoginBasic() => View();

        [HttpGet]
        public IActionResult LoginBasic(string idannonce)
        {
            ViewBag.IdAnnonce = idannonce;
            return View("~/Views/Auth/Login.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> LoginBasic(string email, string password, string? idannonce)
        {
            var user = await _context.Candidat
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                long iduser = user.Id;
                HttpContext.Session.SetInt32("UserId", (int)iduser);
                if (!string.IsNullOrEmpty(idannonce))
                    return RedirectToAction("Postule", "Postule", new { idannonce });
                return RedirectToAction("GetActiveAnnonces", "FirstPage");
            }

            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View("~/Views/Auth/Login.cshtml");
        }

        [HttpGet, ActionName("register")]
        public IActionResult Register(string? idannonce)
        {
            ViewBag.IdAnnonce = idannonce;
            return View("~/Views/Auth/Register.cshtml");
        }

        [HttpPost, ActionName("createCandidat")]
        public async Task<IActionResult> CreateCandidat(string nom, string prenom, string email, string password, IFormFile newcv, int? idannonce)
        {
            var candidat = new Candidat
            {
                Nom = nom,
                Prenom = prenom,
                Email = email,
                Password = password,
            };

            if (newcv != null && newcv.Length > 0)
            {
                var parentDirectory = Directory.GetParent(_env.ContentRootPath);
                if (parentDirectory != null)
                {
                    var backOfficeRoot = Path.Combine(parentDirectory.FullName, "rh.BackOffice", "wwwroot", "cvs", "Téléchargements");
                    Directory.CreateDirectory(backOfficeRoot);

                    // Nettoyer nom/prenom pour éviter les caractères invalides dans le nom de fichier
                    string safeNom = string.Concat(nom.Where(char.IsLetterOrDigit));
                    string safePrenom = string.Concat(prenom.Where(char.IsLetterOrDigit));
                    string extension = Path.GetExtension(newcv.FileName);
                    string newFileName = $"{safeNom}_{safePrenom}{extension}";
                    var filePath = Path.Combine(backOfficeRoot, newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await newcv.CopyToAsync(stream);
                    }
                    candidat.PieceJointe = $"wwwroot/cvs/Téléchargements/{newFileName}";
                    _context.Candidat.Add(candidat);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Gérer le cas où le parent est null (optionnel : log ou throw)
                    // throw new InvalidOperationException("Impossible de trouver le dossier parent du ContentRootPath.");
                }
            }
            return RedirectToAction("LoginBasic", "Auth");
        }

    }
}
