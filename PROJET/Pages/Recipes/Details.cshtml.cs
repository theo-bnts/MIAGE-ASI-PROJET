using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    public Recipe Recipe { get; set; } = default!;
    
    public List<Diet> Diets { get; set; } = default!;
    
    public List<Diet> SelectedDiets { get; set; } = default!;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.Id == id);
        
        if (recipe == null)
            return NotFound();
        
        Recipe = recipe;
        
        Diets = _context.Diet.ToList();
        
        SelectedDiets = (
            await _context.RecipeDiet
                .Where(rd => rd.RecipeId == id)
                .Select(rd => rd.Diet)
                .ToListAsync()
        )!;
        
        return Page();
    }
}