using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.ActivityGroups;

[Authorize(Roles = "ADMINISTRATEUR")]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] public ActivityGroup ActivityGroup { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ActivityGroup = await _context.ActivityGroups.FirstOrDefaultAsync(m => m.Id == id);

        if (ActivityGroup == null)
        {
            return NotFound();
        } 
        else
        {
            this.ActivityGroup = ActivityGroup;
        }
        return Page();
    }
    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ActivityGroup = await _context.ActivityGroups.FindAsync(id);

        if (ActivityGroup == null)
        {
            return NotFound();
        }

        bool isUsed = _context.Activies.Any(a => a.ActivityGroupId == id);

        if (isUsed)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            _context.ActivityGroups.Remove(ActivityGroup);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
