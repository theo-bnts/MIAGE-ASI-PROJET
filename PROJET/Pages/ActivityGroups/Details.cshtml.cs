using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROJET.Data;
using PROJET.Model;

namespace PROJET.Pages.ActivityGroups;

[Authorize(Roles = "ADMINISTRATEUR")]
public class DetailsModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DetailsModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public ActivityGroup? ActivityGroup { get; set; } = default!;
    public IList<Activity> Activities { get; set; } = default!;
    public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    public List<string> AssociatedUserIds { get; set; } = new List<string>();

    public async Task<IActionResult> OnPostAsync(int id, List<string> associatedUsers)
    {

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var currentAssociations = await _context.ApplicationUserActivities
            .Where(ua => ua.ActivityGroupId == id)
            .ToListAsync();

        var toAdd = associatedUsers.Except(currentAssociations.Select(ua => ua.ApplicationUserId));
        foreach (var userId in toAdd)
        {
            _context.ApplicationUserActivities.Add(new ApplicationUserActivity { ApplicationUserId = userId, ActivityGroupId = id });
        }

        var toRemove = currentAssociations.Where(ua => !associatedUsers.Contains(ua.ApplicationUserId));
        _context.ApplicationUserActivities.RemoveRange(toRemove);

        await _context.SaveChangesAsync();

        return RedirectToPage(new { id = id });
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        ActivityGroup = await _context.ActivityGroups
            .Include(ag => ag.Activities)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (ActivityGroup == null)
        {
            return NotFound();
        }

        Activities = ActivityGroup.Activities.ToList();

        Users = await _context.Users
            .Where(u => !_context.UserRoles.Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new { ur.UserId, r.Name })
            .Any(ur => ur.UserId == u.Id && ur.Name == "ADMINISTRATEUR"))
            .ToListAsync();

        AssociatedUserIds = await _context.ApplicationUserActivities
            .Where(ua => ua.ActivityGroupId == id)
            .Select(ua => ua.ApplicationUserId)
            .ToListAsync();

        return Page();
    }
}

