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

namespace PROJET.Pages.SocioProfessionalCategories
{
    [Authorize(Roles = "ADMINISTRATEUR")]
    public class IndexModel : PageModel
    {
        private readonly PROJET.Data.ApplicationDbContext _context;

        public IndexModel(PROJET.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SocioProfessionalCategory> SocioProfessionalCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SocioProfessionalCategory = await _context.SocioProfessionalCategory.ToListAsync();
        }
    }
}
