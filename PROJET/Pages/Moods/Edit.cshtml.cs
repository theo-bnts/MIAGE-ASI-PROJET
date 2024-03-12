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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROJET.Pages.Humeurs
{
    [Authorize]
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
        public Mood Humeur { get; set; } = default!;

        public string UserID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            DateTime dateDuJour = DateTime.Today.Date;

            string dateJour = dateDuJour.ToString("yyyy-MM-dd");

            ViewData["DateDuJour"] = dateJour;

            var user = await _userManager.GetUserAsync(User);
            // Récupérer l'identifiant utilisateur en tant que string
            UserID = user.Id;
            
            ViewData["LutilisateurID"] = UserID;

            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var existingHumeur = await _context.Humeur.FirstOrDefaultAsync(h =>
                h.UtilisateurID == Humeur.ApplicationUserId &&
                h.Date_Humeur == Humeur.Date); 

            if (existingHumeur != null)
            {
                // _context.Entry(existingHumeur).CurrentValues.SetValues(Humeur);
                existingHumeur.Nom_Humeur = Humeur.Name;
            }
            else
            {
                _context.Humeur.Add(Humeur);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HumeurExists(Humeur.ApplicationUserId, Humeur.Date))
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

        private bool HumeurExists(string id, DateTime humeur)
        {
            return _context.Humeur.Any(h => h.UtilisateurID == id && h.Date_Humeur.Date == humeur);
        }
    }
}
