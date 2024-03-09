using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    [BindProperty] public Recipe Recipe { get; set; } = default!;
    
    public required List<Diet> Diets { get; set; }
    
    [BindProperty] public required int[] SelectedDiets { get; set; }

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult OnGet()
    {
        Diets = _context.Diet.ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Diets = _context.Diet.ToList(); // Re-fetch in case of invalid model state
            return Page();
        }

        // Logic to add Recipe first
        _context.Recipe.Add(Recipe);
        await _context.SaveChangesAsync();

        // Handle selected diets
        foreach (var dietId in SelectedDiets)
        {
            var recipeDiet = new RecipeDiet
            {
                RecipeId = Recipe.Id,
                DietId = dietId
            };
            _context.RecipeDiet.Add(recipeDiet);
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}