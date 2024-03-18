using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.ApplicationUserSocioProfessionalCategories
{
    [Authorize(Roles = null)]
    public class EditModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;

        public EditModel(PROJET.Data.ApplicationDbContext context, UserManager<ApplicationUser>
            userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUserSocioProfessionalCategory ApplicationUserSocioProfessionalCategory { get; set; } = default!;

        public string UserID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var categories = await _context.SocioProfessionalCategory.ToListAsync();

            ViewData["LaCategorieID"] = new SelectList(categories, "Id", "Name");

            var user = await _userManager.GetUserAsync(User);
            // Récupérer l'identifiant utilisateur en tant que string
            UserID = user.Id;

            ViewData["LutilisateurID"] = UserID;

            var auspc = await _context.ApplicationUserSocioProfessionalCategory.FirstOrDefaultAsync(m => m.ApplicationUserId == UserID);

            if (auspc == null)
            {
                return Page();
            }
            ApplicationUserSocioProfessionalCategory = auspc;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var existingEmploi = await _context.ApplicationUserSocioProfessionalCategory.FirstOrDefaultAsync(e => e.ApplicationUserId == ApplicationUserSocioProfessionalCategory.ApplicationUserId);

            if (existingEmploi != null)
            {
                _context.Entry(existingEmploi).CurrentValues.SetValues(ApplicationUserSocioProfessionalCategory);

            }
            else
            {
                _context.ApplicationUserSocioProfessionalCategory.Add(ApplicationUserSocioProfessionalCategory);
            }



            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuspcExists(ApplicationUserSocioProfessionalCategory.ApplicationUserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Edit");
        }

        private bool AuspcExists(string id)
        {
            return _context.ApplicationUserSocioProfessionalCategory.Any(e => e.ApplicationUserId == id);
        }
    }

}

