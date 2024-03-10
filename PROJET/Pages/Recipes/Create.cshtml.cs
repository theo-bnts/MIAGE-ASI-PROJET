using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Recipe Recipe { get; set; } = default!;

    public required List<Diet> Diets { get; set; }

    [BindProperty] public required int[] SelectedDiets { get; set; }

    public IActionResult OnGet()
    {
        Diets = _context.Diets.ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return OnGet();
        }
        
        _context.Recipes.Add(Recipe);
        await _context.SaveChangesAsync();
        
        foreach (var dietId in SelectedDiets)
        {
            var recipeDiet = new RecipeDiet
            {
                RecipeId = Recipe.Id,
                DietId = dietId
            };
            _context.RecipesDiets.Add(recipeDiet);
        }
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}