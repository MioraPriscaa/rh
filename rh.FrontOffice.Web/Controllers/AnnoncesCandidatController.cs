using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace rh.FrontOffice.Web.Controllers
{
    public class AnnoncesCandidatController : Controller
    {
        private readonly AppDbContext _context;

        public AnnoncesCandidatController(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            int? idcandidat = HttpContext.Session.GetInt32("UserId");
            if (idcandidat == null)
            {
                return RedirectToAction("LoginBasic", "Auth");
            }

            var candidatures = await _context.Candidature
                .Where(c => c.IdCandidat == idcandidat)
                .Include(c => c.Annonce)
                    .ThenInclude(a => a.TypeContrat)
                .Include(c => c.Annonce)
                    .ThenInclude(a => a.ModeTravail)
                .Include(c=> c.Statut)
                .ToListAsync();


            ViewBag.iduser = idcandidat;
            return View("~/Views/AnnonceCandidat/AnnonceCandidat.cshtml", candidatures);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces
                .Include(a => a.ModeTravail)
                .Include(a => a.TypeContrat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annonce == null)
            {
                return NotFound();
            }

            return View(annonce);
        }

        
        public IActionResult Create()
        {
            ViewData["IdModeTravail"] = new SelectList(_context.ModeTravails, "Id", "Libelle");
            ViewData["IdTypeContrat"] = new SelectList(_context.TypeContrats, "Id", "Libelle");
            return View();
        }

        
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Libelle,Description,CompetenceRequis,IdTypeContrat,IdModeTravail,Duree,NbDossierValide,NiveauExperience,Localisation,DateFin,DateCreation")] Annonce annonce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(annonce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdModeTravail"] = new SelectList(_context.ModeTravails, "Id", "Libelle", annonce.IdModeTravail);
            ViewData["IdTypeContrat"] = new SelectList(_context.TypeContrats, "Id", "Libelle", annonce.IdTypeContrat);
            return View(annonce);
        }

        // GET: Annonces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce == null)
            {
                return NotFound();
            }
            ViewData["IdModeTravail"] = new SelectList(_context.ModeTravails, "Id", "Libelle", annonce.IdModeTravail);
            ViewData["IdTypeContrat"] = new SelectList(_context.TypeContrats, "Id", "Libelle", annonce.IdTypeContrat);
            return View(annonce);
        }

        // POST: Annonces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Libelle,Description,CompetenceRequis,IdTypeContrat,IdModeTravail,Duree,NbDossierValide,NiveauExperience,Localisation,DateFin,DateCreation")] Annonce annonce)
        {
            if (id != annonce.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(annonce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnonceExists(annonce.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdModeTravail"] = new SelectList(_context.ModeTravails, "Id", "Libelle", annonce.IdModeTravail);
            ViewData["IdTypeContrat"] = new SelectList(_context.TypeContrats, "Id", "Libelle", annonce.IdTypeContrat);
            return View(annonce);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonce = await _context.Annonces
                .Include(a => a.ModeTravail)
                .Include(a => a.TypeContrat)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annonce == null)
            {
                return NotFound();
            }

            return View(annonce);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce != null)
            {
                _context.Annonces.Remove(annonce);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnonceExists(int id)
        {
            return _context.Annonces.Any(e => e.Id == id);
        }

        [HttpGet, ActionName("GetActif")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetActif(int id)
        {
            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce == null)
            {
                return NotFound();
            }
            bool isActive = annonce.DateFin == null || annonce.DateFin > DateTime.Now;
            return Json(new { actif = isActive });
        }
    }
}
