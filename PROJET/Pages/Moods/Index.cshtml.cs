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
using PROJET.ViewModels;

namespace PROJET.Pages.Moods
{
    //[Authorize(Roles = "ADMINISTRATEUR")]
    public class IndexModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public IndexModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int? ChoixCategorie { get; set; }

        public List<MoodsOfUsers> MoodsOfUsers { get; set; }

        public IList<Mood> Mood { get; set; } = default!;

        public IList<SocioProfessionalCategory> Categories { get; set; }

        public String Etape { get; set; }

        public async Task OnGetAsync()
        {

            Categories = await _context.SocioProfessionalCategory.ToListAsync();

            Etape = "Choix";
        }

        public async Task<IActionResult> OnPostChargerCategorieAsync()
        {

            if (ChoixCategorie != null)
            {

                // Étape 1: Obtenir tous les utilisateurs de la catégorie choisie
                var Utilisateurs = await _context.ApplicationUserSocioProfessionalCategory
                    .Include(e => e.ListMoods)
                    .Where(e => e.SocioProfessionalCategoryId == ChoixCategorie)
                    .ToListAsync();

                // Récupérer les IDs des utilisateurs
                var idsUtilisateurs = Utilisateurs.Select(u => u.ApplicationUserId).ToList();

                // Étape 2: Filtrer les humeurs en fonction des IDs des utilisateurs récupérés en les affichant dans une vue modèle
                MoodsOfUsers = await _context.Mood
                 .Include(rm => rm.RefMood)
                 .Where(rm => idsUtilisateurs.Contains(rm.ApplicationUserId))
                 .Include(c => c.User)
                 .Where(c => idsUtilisateurs.Contains(c.ApplicationUserId))
                 .Select(rm => new MoodsOfUsers
                 {
                     Id = rm.Id,
                     RefMoodName = rm.RefMood.Name,
                     Date = rm.Date,
                     ApplicationUserId = rm.ApplicationUserId,
                     ApplicationUserMail = rm.User.Email
                 })
                 .ToListAsync();

                Etape = "Visualisation";

                return Page();
            }
            else
            {

                Etape = "Choix";

                return RedirectToPage("./Index");

            }


        }
    }
}