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
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DetailsModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public DetailsModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Mood Mood { get; set; } = default!;

        public IList<Mood> Moods { get; set; } = default!;

        public IList<SocioProfessionalCategory> Categories { get; set; }

        public List<MoodsOfUsers> MoodsOfUsers { get; set; }

        public String Etape { get; set; }

        [BindProperty]
        public int? ChoixCategorie { get; set; }
        public string Moyenne_Content { get; set; }
        public string Moyenne_Mitige { get; set; }
        public string Moyenne_Triste { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Categories = await _context.SocioProfessionalCategory.ToListAsync();

            Etape = "Choix";

            return Page();
        }

        public async Task<IActionResult> OnPostChargerStatistiquesAsync()
        {

            if (ChoixCategorie != null)
            {

                // Étape 1: Obtenir tous les utilisateurs de la catégorie choisie
                var Utilisateurs = await _context.ApplicationUserSocioProfessionalCategory
                    .Include(e => e.ListMoods)
                    .Where(e => e.SocioProfessionalCategoryId == ChoixCategorie)
                    .ToListAsync();

                //await _context.Categorie_sociopro.ToListAsync();

                // Récupérer les IDs des utilisateurs
                var idsUtilisateurs = Utilisateurs.Select(u => u.ApplicationUserId).ToList();

                // Étape 2: Filtrer les humeurs en fonction des IDs des utilisateurs récupérés
                //Moods = await _context.Mood.Where(h => idsUtilisateurs.Contains(h.ApplicationUserId)).ToListAsync();

                MoodsOfUsers = await _context.Mood
                .Include(rm => rm.RefMood)
                .Where(rm => idsUtilisateurs.Contains(rm.ApplicationUserId))
                .Select(rm => new MoodsOfUsers
                {
                    Id = rm.Id,
                    RefMoodName = rm.RefMood.Name,
                    Date = rm.Date,
                    ApplicationUserId = rm.ApplicationUserId
                })
                .ToListAsync();

                double total = 0;
                double total_content = 0;
                double total_mitige = 0;
                double total_triste = 0;

                foreach (MoodsOfUsers mood in MoodsOfUsers)
                {

                    if (mood.RefMoodName == "Content")
                    {
                        total_content++;
                    }

                    if (mood.RefMoodName == "Mitigé")
                    {
                        total_mitige++;
                    }

                    if (mood.RefMoodName == "Triste")
                    {
                        total_triste++;
                    }

                    total++;
                }

                Moyenne_Content = String.Format("{0:0.00}", (total_content / total) * 100);
                Moyenne_Mitige = String.Format("{0:0.00}", (total_mitige / total) * 100);
                Moyenne_Triste = String.Format("{0:0.00}", (total_triste / total) * 100);

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