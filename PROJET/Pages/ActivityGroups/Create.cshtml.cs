using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.ActivityGroups;

[Authorize(Roles = "ADMINISTRATEUR")]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public ActivityGroup ActivityGroup { get; set; } = default!;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.ActivityGroups.Add(ActivityGroup);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
