using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using rh.Domain.Entities;
using rh.Infrastructure.Data;

namespace rh.BackOffice.Pages_Annonces
{
    public class CreateModel : PageModel
    {
        private readonly rh.Infrastructure.Data.AppDbContext _context;

        public CreateModel(rh.Infrastructure.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Annonce Annonce { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Annonces.Add(Annonce);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
