using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;
using System.Security.Claims;

namespace PROJET.Pages.Activities;

[Authorize]
public class IndexModel : PageModel
{
	private readonly ApplicationDbContext _context;

	public IndexModel(ApplicationDbContext context)
	{
		_context = context;
	}

	public IList<Activity> Activities { get; set; } = default!;

    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var activityGroupIds = await _context.ApplicationUserActivities
            .Where(ud => ud.ApplicationUserId == userId)
            .Select(ud => ud.ActivityGroup)
            .ToListAsync();

        Activities = await _context.Activies
            .Where(a => activityGroupIds.Contains(a.ActivityGroup))
            .ToListAsync();
    }

}
