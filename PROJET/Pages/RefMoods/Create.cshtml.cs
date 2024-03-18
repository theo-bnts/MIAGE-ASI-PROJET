using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.RefMoods
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class CreateModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public CreateModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RefMood RefMood { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RefMood.Add(RefMood);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
