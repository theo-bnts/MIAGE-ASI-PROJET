using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.Activities;

[Authorize(Roles = "ADMINISTRATEUR")]
public class DeleteModel : PageModel
{
	private readonly ApplicationDbContext _context;

	public DeleteModel(ApplicationDbContext context)
	{
		_context = context;
	}

	[BindProperty] public Activity Activity { get; set; } = default!;

	public async Task<IActionResult> OnGetAsync(int? id)
	{
		if (id == null) return NotFound();

		var activity = await _context.Activies.FirstOrDefaultAsync(m => m.Id == id);

		if (activity == null) return NotFound();

		Activity = activity;
		return Page();
	}

	public async Task<IActionResult> OnPostAsync(int? id)
	{
		if (id == null) return NotFound();

		var activity = await _context.Activies.FindAsync(id);
		if (activity != null)
		{
			_context.Activies.Remove(activity);
			await _context.SaveChangesAsync();
		}

		return RedirectToPage("./Index");
	}
}
