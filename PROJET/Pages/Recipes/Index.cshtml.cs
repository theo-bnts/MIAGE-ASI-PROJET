using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Recipe> Recipes { get; set; } = default!;

    public IList<Diet> Diets { get; set; } = default!;

    public Dictionary<Recipe, List<Diet>> RecipesDiets { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Recipes = await _context.Recipes.ToListAsync();
        
        Diets = await _context.Diets
            .Include(d => d.RecipesDiet)
            .ToListAsync();

        RecipesDiets = new Dictionary<Recipe, List<Diet>>();

        foreach (var recipe in Recipes)
        {
            var diets = await _context.RecipesDiets
                .Where(rd => rd.RecipeId == recipe.Id)
                .Select(rd => rd.Diet)
                .ToListAsync();
            RecipesDiets.Add(recipe, diets);
        }
    }
}