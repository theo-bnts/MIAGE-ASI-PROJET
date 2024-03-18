using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Activities;

[Authorize(Roles = "ADMINISTRATEUR")]
public class CreateModel : PageModel
{
	private readonly ApplicationDbContext _context;

	public CreateModel(ApplicationDbContext context)
	{
		_context = context;
	}

	[BindProperty]
	public Activity Activity { get; set; } = default!;

	[BindProperty]
	public required List<ActivityGroup> ActivityGroups { get; set; }

	[BindProperty]
	public required int SelectedActivityGroupId { get; set; }

    public IActionResult OnGet()
    {
        ActivityGroups = _context.ActivityGroups.ToList();
        ViewData["ActivityGroupId"] = new SelectList(ActivityGroups, "Id", "Name");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid) return OnGet();

		Activity.ActivityGroupId = SelectedActivityGroupId;
		_context.Activies.Add(Activity);
		await _context.SaveChangesAsync();

		return RedirectToPage("./Index");
	}
}
