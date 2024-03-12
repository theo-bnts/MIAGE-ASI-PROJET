using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Categories_sociopros
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class EditModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public EditModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SocioProfessionalCategory Categorie_sociopro { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie_sociopro =  await _context.Categorie_sociopro.FirstOrDefaultAsync(m => m.ID == id);
            if (categorie_sociopro == null)
            {
                return NotFound();
            }
            Categorie_sociopro = categorie_sociopro;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Categorie_sociopro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Categorie_socioproExists(Categorie_sociopro.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Categorie_socioproExists(int id)
        {
            return _context.Categorie_sociopro.Any(e => e.ID == id);
        }
    }
}
