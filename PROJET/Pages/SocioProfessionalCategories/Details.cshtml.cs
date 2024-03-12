using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Categories_sociopros
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class DetailsModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public DetailsModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public SocioProfessionalCategory Categorie_sociopro { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie_sociopro = await _context.Categorie_sociopro.FirstOrDefaultAsync(m => m.ID == id);
            if (categorie_sociopro == null)
            {
                return NotFound();
            }
            else
            {
                Categorie_sociopro = categorie_sociopro;
            }
            return Page();
        }
    }
}
