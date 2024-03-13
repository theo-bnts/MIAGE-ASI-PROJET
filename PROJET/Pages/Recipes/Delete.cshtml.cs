using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Recipe Recipe { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipes.FirstOrDefaultAsync(m => m.Id == id);

        if (recipe == null)
            return NotFound();

        if (!User.IsInRole("ADMINISTRATEUR") &&
            recipe.ApplicationUserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return Forbid();

        Recipe = recipe;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            Recipe = recipe;

            _context.Recipes.Remove(Recipe);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}