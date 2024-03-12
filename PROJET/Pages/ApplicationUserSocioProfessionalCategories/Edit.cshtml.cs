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
using Microsoft.AspNetCore.Identity;

namespace PROJET.Pages.Emplois
{
    [Authorize(Roles = null)]
    public class EditModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;
        UserManager<IdentityUser> _userManager;

        public EditModel(PROJET.Data.ApplicationDbContext context, UserManager<IdentityUser>
            userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUserSocioProfessionalCategory Emploi { get; set; } = default!;

        public string UserID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var categories = await _context.Categorie_sociopro.ToListAsync();

            ViewData["LaCategorieID"] = new SelectList(categories, "ID", "Nom_categorie");

            var user = await _userManager.GetUserAsync(User);
            // Récupérer l'identifiant utilisateur en tant que string
            UserID = user.Id;
            
            ViewData["LutilisateurID"] = UserID;

            var emploi =  await _context.Emploi.FirstOrDefaultAsync(m => m.LutilisateurID == UserID);
            
            if (emploi == null)
            {
                return Page();
            }
            Emploi = emploi;

            ViewData["LaCategorieID"] = new SelectList(_context.Categorie_sociopro, "ID", "Nom_categorie");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var existingEmploi = await _context.Emploi.FirstOrDefaultAsync( e => e.LutilisateurID == Emploi.ApplicationUserId);

            if (existingEmploi != null)
            {
                _context.Entry(existingEmploi).CurrentValues.SetValues(Emploi);
                
            }
            else
            {
                _context.Emploi.Add(Emploi);
            }

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmploiExists(Emploi.ApplicationUserId))
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

        private bool EmploiExists(string id)
        {
            return _context.Emploi.Any(e => e.LutilisateurID == id);
        }
    }
}
