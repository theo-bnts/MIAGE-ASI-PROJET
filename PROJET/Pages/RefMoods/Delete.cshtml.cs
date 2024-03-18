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

namespace PROJET.Pages.RefMoods
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
        public RefMood RefMood { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refmood = await _context.RefMood.FirstOrDefaultAsync(m => m.Id == id);

            if (refmood == null)
            {
                return NotFound();
            }
            else
            {
                RefMood = refmood;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var refmood = await _context.RefMood.FindAsync(id);
            if (refmood != null)
            {
                RefMood = refmood;
                _context.RefMood.Remove(RefMood);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
