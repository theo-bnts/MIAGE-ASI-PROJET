using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Activities;

[Authorize(Roles = "ADMINISTRATEUR")]
public class EditModel : PageModel
{
	private readonly ApplicationDbContext _context;
	public EditModel(ApplicationDbContext context)
	{
		_context = context;
	}

	[BindProperty] public Activity Activity { get; set; } = default!;

	public List<ActivityGroup> ActivityGroups { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Activity = await _context.Activies
            .FirstOrDefaultAsync(m => m.Id == id);

        if (Activity == null)
        {
            return NotFound();
        }

        ActivityGroups = await _context.ActivityGroups.ToListAsync();

        ViewData["ActivityGroupId"] = new SelectList(ActivityGroups, "Id", "Name", Activity.ActivityGroupId);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
	{
		if (!ModelState.IsValid) return Page();

		_context.Attach(Activity).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!_context.Activies.Any(e => e.Id == Activity.Id))
				return NotFound();
			throw;
		}
		return RedirectToPage("./Index");
	}
}
