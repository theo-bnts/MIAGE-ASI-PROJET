using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Recipe Recipe { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null)
            return NotFound();
        Recipe = recipe;
        return Page();
    }
}