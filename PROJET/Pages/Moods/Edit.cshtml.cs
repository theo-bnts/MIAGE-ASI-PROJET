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
using PROJET.Data.Migrations;
using PROJET.Model;

namespace PROJET.Pages.Moods
{
    [Authorize]
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
        public Mood Mood { get; set; } = default!;

        public string UserID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var ListMoods = await _context.RefMood.ToListAsync();

            ViewData["ListMoodID"] = new SelectList(ListMoods, "Id", "Name");

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

            var existingHumeur = await _context.Mood.FirstOrDefaultAsync(h =>
                h.ApplicationUserId == Mood.ApplicationUserId &&
                h.Date == Mood.Date);

            Console.WriteLine(Mood.Date);

            if (existingHumeur != null)
            {
                existingHumeur.RefMoodId = Mood.RefMoodId;
            }
            else
            {
                _context.Mood.Add(Mood);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoodExists(Mood.ApplicationUserId, Mood.Date))
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

        private bool MoodExists(string id, DateTime humeur)
        {
            return _context.Mood.Any(h => h.ApplicationUserId == id && h.Date.Date == humeur);
        }
    }
}
