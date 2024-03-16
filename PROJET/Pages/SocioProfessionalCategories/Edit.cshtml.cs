using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.SocioProfessionalCategories
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
        public SocioProfessionalCategory SocioProfessionalCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socioprofessionalcategory =  await _context.SocioProfessionalCategory.FirstOrDefaultAsync(m => m.Id == id);
            if (socioprofessionalcategory == null)
            {
                return NotFound();
            }
            SocioProfessionalCategory = socioprofessionalcategory;
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

            _context.Attach(SocioProfessionalCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocioProfessionalCategoryExists(SocioProfessionalCategory.Id))
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

        private bool SocioProfessionalCategoryExists(int id)
        {
            return _context.SocioProfessionalCategory.Any(e => e.Id == id);
        }
    }
}
