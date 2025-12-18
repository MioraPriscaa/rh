using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Pages_Annonces
{
    public class IndexModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public IndexModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Annonce> Annonce { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Annonce = await _context.Annonces
                .Include(a => a.ModeTravail)
                .Include(a => a.TypeContrat).ToListAsync();
        }
    }
}
