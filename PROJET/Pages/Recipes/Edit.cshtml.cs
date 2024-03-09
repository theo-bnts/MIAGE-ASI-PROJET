using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;
    
    [BindProperty] public Recipe Recipe { get; set; } = default!;
    
    public List<Diet> Diets { get; set; } = default!;
    
    [BindProperty] public required int[] SelectedDiets { get; set; }

    public EditModel(ApplicationDbContext context)
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
                .Select(rd => rd.DietId)
                .ToArrayAsync()
        )!;
        
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            Diets = _context.Diet.ToList(); // Re-fetch in case of invalid model state
            return Page();
        }

        _context.Attach(Recipe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeExists(Recipe.Id))
                return NotFound();
            throw;
        }
        
        // Handle selected diets
        var existingRecipeDiets = await _context.RecipeDiet
            .Where(rd => rd.RecipeId == Recipe.Id)
            .ToListAsync();
        
        _context.RecipeDiet.RemoveRange(existingRecipeDiets);
        
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

    private bool RecipeExists(int id)
    {
        return _context.Recipe.Any(e => e.Id == id);
    }
}