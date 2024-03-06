using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Recipes;

public class EditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Recipe Recipe { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var recipe = await _context.Recipe.FirstOrDefaultAsync(m => m.Id == id);
        if (recipe == null) return NotFound();
        Recipe = recipe;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

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

        return RedirectToPage("./Index");
    }

    private bool RecipeExists(int id)
    {
        return _context.Recipe.Any(e => e.Id == id);
    }
}