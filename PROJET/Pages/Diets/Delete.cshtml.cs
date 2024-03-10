using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Diets;

[Authorize(Roles = "ADMINISTRATEUR")]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Diet Diet { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var diet = await _context.Diets.FirstOrDefaultAsync(m => m.Id == id);

        if (diet == null)
            return NotFound();
        Diet = diet;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();

        var diet = await _context.Diets.FindAsync(id);
        if (diet != null)
        {
            Diet = diet;
            _context.Diets.Remove(Diet);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}