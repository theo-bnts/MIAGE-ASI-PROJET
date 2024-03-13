using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

[Authorize]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Recipe Recipe { get; set; } = default!;

    public List<Diet> Diets { get; set; } = default!;

    [BindProperty] public required int[] SelectedDiets { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        var recipe = await _context.Recipes
            .Include(r => r.RecipeDiets)!
            .ThenInclude(recipeDiet => recipeDiet.Diet)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (recipe == null)
            return NotFound();

        if (!User.IsInRole("ADMINISTRATEUR") &&
            recipe.ApplicationUserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return Forbid();

        Recipe = recipe;

        Diets = _context.Diets.ToList();

        SelectedDiets = Recipe.RecipeDiets!
            .Select(rd => rd.DietId)
            .ToArray();

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (!ModelState.IsValid) return await OnGetAsync(id);

        _context.Attach(Recipe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Recipes.Any(r => r.Id == Recipe.Id))
                return NotFound();

            throw;
        }

        var existingRecipeDiets = await _context.RecipesDiets
            .Where(rd => rd.RecipeId == Recipe.Id)
            .ToListAsync();

        _context.RecipesDiets.RemoveRange(existingRecipeDiets);

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