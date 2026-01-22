using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rh.BackOffice.Pages_Annonces
{
    public class IndexModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;
        public string DateCreationSort { get; set; }
        public string CurrentSort { get; set; }

        public IndexModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Annonce> Annonce { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder)
        {
            CurrentSort = sortOrder;
            DateCreationSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";

            IQueryable<Annonce> annonces = _context.Annonces
                .Include(a => a.TypeContrat)
                .Include(a => a.ModeTravail);

            annonces = sortOrder switch
            {
                "date_desc" => annonces.OrderByDescending(a => a.DateCreation),
                "date_asc" => annonces.OrderBy(a => a.DateCreation),
                _ => annonces.OrderByDescending(a => a.DateCreation) // défaut
            };

            Annonce = await annonces.ToListAsync();
        }
    }
}
