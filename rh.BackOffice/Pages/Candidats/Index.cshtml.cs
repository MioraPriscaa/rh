using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Pages_Candidats
{
    public class IndexModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public IndexModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Candidat> Candidat { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Candidat = await _context.Candidat.ToListAsync();
        }
    }
}
