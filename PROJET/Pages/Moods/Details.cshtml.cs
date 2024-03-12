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

namespace PROJET.Pages.Humeurs
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DetailsModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public DetailsModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Mood> Humeur { get; set; } = default!;

        public IList<SocioProfessionalCategory> Categories { get; set; }

        public String Etape { get; set; }

        [BindProperty]
        public int? ChoixCategorie { get; set; }
        public double Moyenne_Content { get; set; }
        public double Moyenne_Mitige { get; set; }
        public double Moyenne_Triste { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Categories = await _context.Categorie_sociopro.ToListAsync();

            Etape = "Choix";

            /* if (id == null)
             {
                 return NotFound();
             }

             var humeur = await _context.Humeur.FirstOrDefaultAsync(m => m.ID == id);
             if (humeur == null)
             {
                 return NotFound();
             }
             else
             {
                 Humeur = humeur;
             }*/


            return Page();
        }

        public async Task<IActionResult> OnPostChargerStatistiquesAsync()
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
                Humeur = await _context.Humeur.Where(h => idsUtilisateurs.Contains(h.UtilisateurID)).ToListAsync();

                double total = 0;
                double total_content = 0;
                double total_mitige = 0;
                double total_triste = 0;

                foreach (Mood humeur in Humeur)
                {
                   
                    if (humeur.Name == "Content")
                    {
                        total_content++;
                    }

                    if (humeur.Name == "Mitigé")
                    {
                        total_mitige++;
                    }

                    if (humeur.Name == "Triste")
                    {
                        total_triste++;
                    }

                    total++;
                }


                 Moyenne_Content = (total_content / total)*100;
                 Moyenne_Mitige = (total_mitige / total)*100;
                 Moyenne_Triste = (total_triste / total)*100;

                Console.WriteLine(Moyenne_Content);
                Console.WriteLine(Moyenne_Mitige);
                Console.WriteLine(Moyenne_Triste);

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
