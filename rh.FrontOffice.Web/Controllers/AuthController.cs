using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rh.Infrastructure.Data;

namespace rh.FrontOffice.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
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
                return RedirectToAction("Index", "FirstPage");
            }

            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View("~/Views/Auth/Login.cshtml");
        }

    }
}
