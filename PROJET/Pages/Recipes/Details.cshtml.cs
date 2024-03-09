using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class DetailsModel(ApplicationDbContext context) : PageModel
{
    public Recipe Recipe { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var recipe = await context.Recipe
            .Include(r => r.RecipeDiets)!
            .ThenInclude(recipeDiet => recipeDiet.Diet)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (recipe == null)
            return NotFound();

        Recipe = recipe;

        return Page();
    }
}