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
    public class IndexModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public IndexModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SocioProfessionalCategory> Categorie_sociopro { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Categorie_sociopro = await _context.Categorie_sociopro.ToListAsync();
        }
    }
}
