using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Diets;

[Authorize(Roles = "ADMINISTRATEUR")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Diet> Diet { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Diet = await _context.Diets.ToListAsync();
    }
}