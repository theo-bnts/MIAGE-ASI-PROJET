using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.SocioProfessionalCategories
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DeleteModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public DeleteModel(PROJET.Data.ApplicationDbContext context)
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

            var socioprofessionalcategory = await _context.SocioProfessionalCategory.FirstOrDefaultAsync(m => m.Id == id);

            if (socioprofessionalcategory == null)
            {
                return NotFound();
            }
            else
            {
                SocioProfessionalCategory = socioprofessionalcategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socioprofessionalcategory = await _context.SocioProfessionalCategory.FindAsync(id);
            if (socioprofessionalcategory != null)
            {
                SocioProfessionalCategory = socioprofessionalcategory;
                _context.SocioProfessionalCategory.Remove(SocioProfessionalCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
