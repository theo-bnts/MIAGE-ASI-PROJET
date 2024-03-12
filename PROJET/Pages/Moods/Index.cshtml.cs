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

namespace PROJET.Pages.Humeurs
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class IndexModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public IndexModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int? ChoixCategorie { get; set; }

        public IList<Mood> Humeur { get; set; } = default!;

        public IList<SocioProfessionalCategory> Categories { get; set; }

        public String Etape { get; set; }

        public async Task OnGetAsync()
        {

            Categories = await _context.Categorie_sociopro.ToListAsync();

            Etape = "Choix";

        }

        public async Task<IActionResult> OnPostChargerCategorieAsync()
        {

            if (ChoixCategorie != null)
            {

                // Étape 1: Obtenir tous les utilisateurs de la catégorie choisie
                var Utilisateurs = await _context.Emploi
                    .Include(e => e.LesHumeurs)
                    .Where(e => e.LaCategorieID == ChoixCategorie)
                    .ToListAsync();

                //await _context.Categorie_sociopro.ToListAsync();

                // Récupérer les IDs des utilisateurs
                var idsUtilisateurs = Utilisateurs.Select(u => u.LutilisateurID).ToList();

                // Étape 2: Filtrer les humeurs en fonction des IDs des utilisateurs récupérés
                Humeur = await _context.Humeur
                    .Where(h => idsUtilisateurs.Contains(h.UtilisateurID)) // Supposition que Humeur a une propriété UtilisateurId
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
