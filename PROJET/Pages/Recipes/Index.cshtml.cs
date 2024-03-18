using System.Security.Claims;
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
        // Load Diets
        var diets = await _context.Diets.ToListAsync();

        var forYouDiet = new Diet
        {
            Id = -1,
            Name = "FOR YOU"
        };
        diets.Insert(0, forYouDiet);

        var noSpecificDiet = new Diet
        {
            Id = -2,
            Name = "NO SPECIFIC DIET"
        };
        diets.Add(noSpecificDiet);

        Diets = diets;

        // Load Recipes with Diets
        Recipes = await _context.Recipes
            .Include(r => r.RecipeDiets)!
            .ThenInclude(rd => rd.Diet)
            .ToListAsync();

        var recipesDiets = new Dictionary<Recipe, List<Diet>>();

        var userDiets = await _context.ApplicationUsersDiets
            .Where(apd => apd.ApplicationUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            .Select(apd => apd.Diet)
            .ToListAsync();

        foreach (var recipe in Recipes)
        {
            var recipeDiets = recipe.RecipeDiets!.Select(rd => rd.Diet).ToList();

            if (userDiets.All(ud => recipeDiets.Contains(ud))) recipeDiets.Add(forYouDiet);

            recipeDiets.Add(noSpecificDiet);

            recipesDiets.Add(recipe, recipeDiets!);
        }

        RecipesDiets = recipesDiets;
    }
}